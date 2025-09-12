using PokedexApi.Dtos;
using PokedexApi.Infrastructure.Soap.Dtos;
using PokedexApi.Models;

namespace PokedexApi.Mappers;

public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonResponseDto pokemonRepsonseDto)
    {
        return new Pokemon
        {
            Id = pokemonRepsonseDto.Id,
            Name = pokemonRepsonseDto.Name,
            Type = pokemonRepsonseDto.Type,
            Level = pokemonRepsonseDto.Level,
            Stats = new Stats
            {
                Attack = pokemonRepsonseDto.Stats.Attack,
                Defense = pokemonRepsonseDto.Stats.Defense,
                Speed = pokemonRepsonseDto.Stats.Speed
            }
        };
    }

    public static PokemonResponse ToResponse(this Pokemon pokemon)
    {
        return new PokemonResponse
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Attack = pokemon.Stats.Attack
        };
    }
}