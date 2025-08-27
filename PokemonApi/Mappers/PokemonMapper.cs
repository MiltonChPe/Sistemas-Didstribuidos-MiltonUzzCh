using PokemonApi.Dtos;
using PokemonApi.Infrastructure.Entities;
using PokemonApi.Models;
namespace PokemonApi.Mappers;


public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonEntity pokemonEntity)
    {
        if (pokemonEntity == null) return null;

        return new Pokemon
        {
            Id = pokemonEntity.Id,
            Name = pokemonEntity.Name,
            Type = pokemonEntity.Type,
            Level = pokemonEntity.Level,
            Stats = new Stats
            {
                Attack = pokemonEntity.Attack,
                Defense = pokemonEntity.Defense,
                Speed = pokemonEntity.Speed,
                Health = pokemonEntity.Health
            }
        };

    }

    public static PokemonEntity ToEntity(this Pokemon pokemon)
    {
        if (pokemon == null) return null;

        return new PokemonEntity
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Attack = pokemon.Stats.Attack,
            Defense = pokemon.Stats.Defense,
            Speed = pokemon.Stats.Speed,
            Health = pokemon.Stats.Health
        };
    }


    public static Pokemon ToModel(this CreatePokemonDto requestPokemonDto)
    {
        return new Pokemon
        {
            Name = requestPokemonDto.Name,
            Type = requestPokemonDto.Type,
            Level = requestPokemonDto.Level,
            Stats = new Stats
            {
                Attack = requestPokemonDto.Stats.Attack,
                Defense = requestPokemonDto.Stats.Defense,
                Speed = requestPokemonDto.Stats.Speed,
                Health = requestPokemonDto.Stats.Health
            }
        };
    }
    public static PokemonResponseDto ToResponseDto(this Pokemon pokemon)
    {

        return new PokemonResponseDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Stats = new StatsDto
            {
                Attack = pokemon.Stats.Attack,
                Defense = pokemon.Stats.Defense,
                Speed = pokemon.Stats.Speed,
                Health = pokemon.Stats.Health
            }
        };
    }
}