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
            Type = pokemon.Type,
            Attack = pokemon.Stats.Attack
  
        };
    }

    public static IList<PokemonResponse> ToResponse(this IList<Pokemon> pokemons)
    {
        return pokemons.Select(s => s.ToResponse()).ToList();
    }

    public static Pokemon ToModel(this CreatePokemonRequest createPokemonRequest)
    {
        return new Pokemon
        {
            Name = createPokemonRequest.Name,
            Type = createPokemonRequest.Type,
            Level = createPokemonRequest.Level,

            Stats = new Stats
            {
                Attack = createPokemonRequest.Stats.Attack,
                Defense = createPokemonRequest.Stats.Defense,
                Speed = createPokemonRequest.Stats.Speed
            }
        };
    }

    public static IList<Pokemon> ToModel(this IList<PokemonResponseDto> pokemonRepsonseDtos)
    {

        return pokemonRepsonseDtos.Select(s => s.ToModel()).ToList();
    }

    public static CreatePokemonDto ToRequest(this Pokemon pokemon)
    { 
        return new CreatePokemonDto
        {
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Stats = new StatsDto
            {
                Attack = pokemon.Stats.Attack,
                Defense = pokemon.Stats.Defense,
                Speed = pokemon.Stats.Speed
            }
        };
    }
}