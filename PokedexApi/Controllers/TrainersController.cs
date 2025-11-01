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