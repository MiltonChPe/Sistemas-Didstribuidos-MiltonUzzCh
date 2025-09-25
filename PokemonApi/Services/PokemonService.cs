using System.ServiceModel;
using PokemonApi.Dtos;
using PokemonApi.Repositories;
namespace PokemonApi.Services;

using PokemonApi.Models;

using PokemonApi.Mappers;
using PokemonApi.Validators;

public class PokemonService : IPokemonService
{

    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<IList<PokemonResponseDto>> GetPokemonsByName(string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonsByNameAsync(name, cancellationToken);
        return pokemon.ToResponseDto();

    }
    public async Task<PagedPokemonResponseDto> GetPokemons(QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        return await _pokemonRepository.GetPokemonsAsync(queryParameters, cancellationToken);
    }
    public async Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemonToUpdate, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(pokemonToUpdate.Id, cancellationToken);
        if (!PokemonExist(pokemon))
        {
            throw new FaultException("Pokemon Not found");
        }

        if (!await IsPokemonAllowedToBeUpdate(pokemonToUpdate, cancellationToken))
        {
            throw new FaultException("Pokemon with this name alrwady exist");
        }
        pokemon.Name = pokemonToUpdate.Name;
        pokemon.Type = pokemonToUpdate.Type;
        pokemon.Stats.Attack = pokemonToUpdate.Stats.Attack;
        pokemon.Stats.Defense = pokemonToUpdate.Stats.Defense;
        pokemon.Stats.Speed = pokemonToUpdate.Stats.Speed;

        await _pokemonRepository.UpdatePokemonAsync(pokemon, cancellationToken);
        return pokemon.ToResponseDto();

    }
    private async Task<bool> IsPokemonAllowedToBeUpdate(UpdatePokemonDto pokemonToUpdate, CancellationToken cancellationToken)
    {
        var duplicatedPokemon = await _pokemonRepository.GetByNameAsync(pokemonToUpdate.Name, cancellationToken);
        return duplicatedPokemon is null || IsTheSamePokemon(duplicatedPokemon, pokemonToUpdate);
    }

    private static bool IsTheSamePokemon(Pokemon pokemon, UpdatePokemonDto pokemonToUpdate)
    {
        return pokemon.Id == pokemonToUpdate.Id;
    }

    public async Task<DeletePokemonResponseDto> DeletePokemon(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
        if (!PokemonExist(pokemon))
        {
            throw new FaultException("Pokemon not fuound");

        }

        await _pokemonRepository.DeletePokemonAsync(pokemon, cancellationToken);
        return new DeletePokemonResponseDto { Success = true };
    }
    public async Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
        return PokemonExist(pokemon) ? pokemon.ToResponseDto() : throw new FaultException("El pokemon no existe");
    }


    public async Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemonRequest, CancellationToken cancellationToken)
    {
        //fluent method
        pokemonRequest.ValidateName().ValidateType().ValidateLevel();

        if (await PokemonAlreadyExist(pokemonRequest.Name, cancellationToken))
        {
            throw new FaultException("El pokemon ya existe");
        }

        var pokemon = await _pokemonRepository.CreateAsync(pokemonRequest.ToModel(), cancellationToken);

        return pokemon.ToResponseDto();
    }

    private static bool PokemonExist(Pokemon pokemon)
    {
        return pokemon != null;
    }

    private async Task<bool> PokemonAlreadyExist(string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetByNameAsync(name, cancellationToken);
        return pokemon != null;
    }
    
    
}