using System.ServiceModel;
using PokemonApi.Dtos;
using PokemonApi.Repositories;
namespace PokemonApi.Services;

using PokemonApi.Mappers;
using PokemonApi.Validators;

public class PokemonService : IPokemonService
{

    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemonRequest, CancellationToken cancellationToken)
    {
        //fluent method
        pokemonRequest.ValidateName().ValidateType().ValidateLevel();

        if (await PokemonAlreadyExist(pokemonRequest.Name, cancellationToken))
        {
            throw new FaultException("El pokemon ya existe");
        }

        var pokemon = await _pokemonRepository.CreateAsync (pokemonRequest.ToModel(), cancellationToken);

        return pokemon.ToResponseDto();
    }

    private async Task<bool> PokemonAlreadyExist(string name,  CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetByNameAsync(name, cancellationToken);
        return pokemon != null;
    }
}