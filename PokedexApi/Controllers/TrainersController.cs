using Microsoft.AspNetCore.Mvc;
using PokedexApi.Exceptions;
using PokedexApi.Infrastructure.Grpc;
using PokedexApi.Models;
using PokedexApi.Dtos;
using Medal = PokedexApi.Models.Medal;
using PokedexApi.Services;

namespace PokedexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TrainersController : ControllerBase
{

    private readonly ITrainerService _trainerService;

    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainerResponseDto>> GetTrainerById(string id, CancellationToken cancellationToken)
    {
        try
        {
            var trainer = await _trainerService.GetByIdAsync(id, cancellationToken);
            return Ok(ToDto(trainer));
        }
        catch (TrainerNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainerResponseDto>>> GetTrainers([FromQuery] string name, CancellationToken cancellationToken)
    {
        var trainers = await _trainerService.GetAllByNameAsync(name, cancellationToken);
        return Ok(ToDto(trainers));

    }

    [HttpPost]
    public async Task<IActionResult> CreateTrainers([FromBody] List<CreateTrainerRequestDto> request, CancellationToken cancellationToken)
    {
        var trainers = ToModel(request);
        var (SuccessCount, createdTrainers) = await _trainerService.CreateTrainersAsync(trainers, cancellationToken);
        return Ok(new { SuccessCount, trainers = ToDto(createdTrainers) });

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrainer(string id, CancellationToken cancellationToken)
    {
        try
        {
            await _trainerService.DeleteTrainerAsync(id, cancellationToken);
            return NoContent();
        }
        catch (TrainerNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message }); //404
        }
        catch (TrainerInvalidIdException ex)
        {
            return BadRequest(new { Message = ex.Message }); //400
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrainer(string id, [FromBody] UpdateTrainerRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            var trainer = ToModel(id, request);
            await _trainerService.UpdateTrainerAsync(trainer, cancellationToken);
            return NoContent();
        }
        catch (TrainerNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message }); //404
        }
        catch (TrainerValidationException ex)
        {
            return BadRequest(new { Message = ex.Message }); //400
        }
        catch (TrainerAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message }); //409
        }

    }
    
    private static Trainer ToModel(string id, UpdateTrainerRequestDto request)
    {
        return new Trainer
        {
            Id = id,
            Name = request.Name,
            Age = request.Age,
            Birthdate = request.Birthdate,
            Medals = request.Medals.Select(m => new Medal
            {
                Region = m.Region,
                MedalType = Enum.Parse<Models.MedalType>(m.Type)
            }).ToList()
        };
    }
    
    private static IEnumerable<Trainer> ToModel(List<CreateTrainerRequestDto> trainers)
    {
        return trainers.Select(trainer => new Trainer
        {
            Name = trainer.Name,
            Age = trainer.Age,
            Birthdate = trainer.Birthdate,
            Medals = trainer.Medals.Select(medal => new Medal
            {
                Region = medal.Region,
                MedalType = Enum.Parse<Models.MedalType>(medal.Type)
            }).ToList()
        });
    }

    private static IEnumerable<TrainerResponseDto> ToDto(IEnumerable<Trainer> trainers)
    {
        return trainers.Select(ToDto);
    }

    public static TrainerResponseDto ToDto(Trainer trainer)
    {
        return new TrainerResponseDto
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Age = trainer.Age,
            Birthdate = trainer.Birthdate,
            CreatedAt = trainer.CreatedAt, 
            Medals = trainer.Medals.Select(ToDto).ToList()
        };
    }

    private static MedalDto ToDto(Medal medal)
    {
        return new MedalDto
        {
            Region = medal.Region,
            Type =  medal.MedalType.ToString()
        };
    }
}