using Grpc.Core;
using PlayerLockerApi.Repositories;
using PlayerLockerApi.Mappers;
using Google.Protobuf.WellKnownTypes;


namespace PlayerLockerApi.Services;

public class LockerService : PlayerLockerApi.LockerService.LockerServiceBase
{
    private readonly ILockerRepository _lockerRepository;

    public LockerService(ILockerRepository lockerRepository)
    {
        _lockerRepository = lockerRepository;
    }

    public override async Task<LockerResponse> GetLockerById(LockerByIdRequest request, ServerCallContext context)
    {
        var locker = await _lockerRepository.GetByIdAsync(request.Id, context.CancellationToken);
        if (locker == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Locker with ID {request.Id} not found."));
        }
        return locker.ToResponseGet();
    }

    public override async Task GetAllLockersByName(TrainerByNameRequest request, IServerStreamWriter<LockerResponse> responseStream, ServerCallContext context)
    {
        var lockers = await _lockerRepository.GetByNameAsync(request.Name, context.CancellationToken);
        foreach (var locker in lockers)
        {
            if (context.CancellationToken.IsCancellationRequested)
            {
                break;
            }
            await responseStream.WriteAsync(locker.ToResponseGet());
            await Task.Delay(TimeSpan.FromSeconds(5), context.CancellationToken);
        }
    }

    public override async Task<CreateLockerResponse> CreateLockers(IAsyncStreamReader<CreateLockerRequest> requestStream, ServerCallContext context)
    {
        var createdLockers = new List<Locker>();
       
        while (await requestStream.MoveNext(context.CancellationToken))
        {
            var request = requestStream.Current;
            var locker = request.ToModel();
            var lockerexists = await _lockerRepository.GetByNameAsync(locker.Name, context.CancellationToken);
            if (lockerexists.Any())
            {
                continue; 
            }
            var createdLocker = await _lockerRepository.CreateAsync(locker, context.CancellationToken);
            createdLockers.Add(createdLocker.ToResponse());
        }

        return new CreateLockerResponse
        {   
            Lockers = { createdLockers }
        };
    }

    public override async Task<Empty> DeleteLocker(LockerByIdRequest request, ServerCallContext context)
    {
        if (!IdFormatIsInvalid(request.Id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, $"The provided ID {request.Id} is not a valid format."));
        }

        var locker = await GetLockerAsync(request.Id, context.CancellationToken);
        await _lockerRepository.DeleteAsync(request.Id, context.CancellationToken);
        return new Empty();
    }

    public override async Task<Empty> UpdateLocker(UpdateLockerRequest request, ServerCallContext context)
    {
        if (!IdFormatIsInvalid(request.Id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, $"The Id {request.Id} is not a valid format."));
        }
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 2)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "The locker name cannot be empty."));
        }
        if (string.IsNullOrWhiteSpace(request.Skin))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "The skin field cannot be empty."));
        }
        if (string.IsNullOrWhiteSpace(request.Pickaxe))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "The pickaxe field cannot be empty."));
        }
        if (string.IsNullOrWhiteSpace(request.Glider))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "The glider field cannot be empty."));
        }
        if (string.IsNullOrWhiteSpace(request.Contrail))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "The contrail field cannot be empty."));
        }
        if (string.IsNullOrWhiteSpace(request.Emote))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "The emote field cannot be empty."));
        }
        if (string.IsNullOrWhiteSpace(request.Backblings))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "The backblings field cannot be empty."));
        }
        

        var locker = await GetLockerAsync(request.Id, context.CancellationToken);
        locker.Name = request.Name;
        locker.Skin = request.Skin;
        locker.Backblings = request.Backblings;
        locker.Pickaxe = request.Pickaxe;
        locker.Glider = request.Glider;
        locker.Contrail = request.Contrail;
        locker.Emote = request.Emote;

        var lockers = await _lockerRepository.GetByNameAsync(request.Name, context.CancellationToken);
        var duplicateLocker = lockers.Any(l => l.Id != request.Id);
        if (duplicateLocker)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, $"A locker with the name {request.Name} already exists."));
        }

        await _lockerRepository.UpdateAsync(locker, context.CancellationToken);
        return new Empty();
    }

    private async Task<Locker> GetLockerAsync(string id, CancellationToken cancellationToken)
    {
        var locker = await _lockerRepository.GetByIdAsync(id, cancellationToken);
        return locker ?? throw new RpcException(new Status(StatusCode.NotFound, $"Locker with ID {id} not found."));
    }
    private static bool IdFormatIsInvalid(string id)
    {
        return !string.IsNullOrWhiteSpace(id) && id.Length > 20;
    }
}