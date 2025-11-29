using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TrainerApi.Events;
using TrainerApi.Infrastructure.Documents;
using TrainerApi.Infrastructure.Producers;
using TrainerApi.Mappers;
using TrainerApi.Models;
using TrainerApi.Repositories;

namespace TrainerApi.Services;

public class TrainerService : TrainerApi.TrainerService.TrainerServiceBase
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IMessageBrokerProducer _producer;
    public TrainerService(ITrainerRepository trainerRepository, IMessageBrokerProducer producer)
    {
        _trainerRepository = trainerRepository;
        _producer = producer;

    }
    public override async Task<TrainerResponse> GetTrainerById(TrainerByIdRequest request, ServerCallContext context)
    {
        var trainer = await _trainerRepository.GetByIdAsync(request.Id, context.CancellationToken);
        if (trainer is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Trainer with ID {request.Id} not found."));

        return trainer.ToResponse();
    }


    public override async Task GetAllTrainersByName(TrainerByNameRequest request, IServerStreamWriter<TrainerResponse> responseStream, ServerCallContext context)
    {
        var trainers = await _trainerRepository.GetByNameAsync(request.Name, context.CancellationToken);
        foreach (var trainer in trainers)
        {
            if (context.CancellationToken.IsCancellationRequested)
            {
                break;
            }
            await responseStream.WriteAsync(trainer.ToResponse());
            await Task.Delay(TimeSpan.FromSeconds(5), context.CancellationToken);
        }
    }


    public override async Task<CreateTrainerResponse> CreateTrainers(IAsyncStreamReader<CreateTrainerRequest> requestStream, ServerCallContext context)
    {
        var createdTrainers = new List<TrainerResponse>();

        while (await requestStream.MoveNext(context.CancellationToken))
        {
            var request = requestStream.Current;
            var trainer = request.ToModel();
            var trainerexists = await _trainerRepository.GetByNameAsync(trainer.Name, context.CancellationToken);
            if (trainerexists.Any())
            {
                continue;
            }
            var createdTrainer = await _trainerRepository.CreateAsync(trainer, context.CancellationToken);
            createdTrainers.Add(createdTrainer.ToResponse());

            var ev = new TrainerCreatedEvent
            {
                Id = createdTrainer.Id,
                Name = createdTrainer.Name,
                Age = createdTrainer.Age,
                BirthDate = createdTrainer.Birthdate,
                CreatedAt = createdTrainer.CreatedAt,
                Medals = createdTrainer.Medals.Select(m => new MedalEvent
                {
                    Region = m.Region,
                    Type = m.Type.ToString()
                }).ToList()
            };

            await _producer.ProduceAsync(ev, cancellationToken: context.CancellationToken);
        }

        return new CreateTrainerResponse
        {
            SuccessCount = createdTrainers.Count,
            Trainers = { createdTrainers }
        };
    }

    public override async Task<Empty> DeleteTrainer(TrainerByIdRequest request, ServerCallContext context)
    {
        if (!IdFormatIsValid(request.Id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "invalid ID format."));
        }

        var trainer = await GetTrainerAsync(request.Id, context.CancellationToken);
        await _trainerRepository.DeleteAsync(request.Id, context.CancellationToken);        
        
        var ev = new TrainerDeletedEvent
        {
            Id = trainer.Id,
            Name = trainer.Name,
            DeletedAt = DateTime.UtcNow
        };

        await _producer.ProduceAsync(ev, cancellationToken: context.CancellationToken);

        return new Empty();

    }
    
    public override async Task<Empty> UpdateTrainer(UpdateTrainerRequest request, ServerCallContext context)
    {
        if (!IdFormatIsValid(request.Id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "invalid ID format."));
        }
        if (request.Age < 18)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Trainer Age."));
        }
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 3)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Trainer Name cannot be empty."));
        }

        var trainer = await GetTrainerAsync(request.Id, context.CancellationToken);
        trainer.Name = request.Name;
        trainer.Age = request.Age;
        trainer.Birthdate = request.Birthdate.ToDateTime();
        trainer.Medals = request.Medals.Select(m => m.ToModel()).ToList();
        
        var trainers = await _trainerRepository.GetByNameAsync(request.Name, context.CancellationToken);
        var trainerAlreadyExists = trainers.Any(t => t.Id != trainer.Id);
        if (trainerAlreadyExists)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, $"Trainer with Name {request.Name} already exists."));
        }
        await _trainerRepository.UpdateAsync(trainer, context.CancellationToken);

        var ev = new TrainerUpdatedEvent
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Age = trainer.Age,
            BirthDate = trainer.Birthdate,
            CreatedAt = trainer.CreatedAt,
            Medals = trainer.Medals.Select(m => new MedalEvent
            {
                Region = m.Region,
                Type = m.Type.ToString()
            }).ToList()
        };

        await _producer.ProduceAsync(ev, cancellationToken: context.CancellationToken);

        return new Empty();
    }
    private async Task<Trainer> GetTrainerAsync(string id, CancellationToken cancellationToken)
    {
        var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken);
        return trainer ?? throw new RpcException(new Status(StatusCode.NotFound, $"Trainer with ID {id} not found."));
    }

    private static bool IdFormatIsValid(string id)
    {
        return !string.IsNullOrWhiteSpace(id) && id.Length > 20;
    }   
}