namespace PokedexApi.Services;

using PokedexApi.Models;
using PokedexApi.Gateways;

public class PokemonService : IPokemonService
{
    private readonly IPokemonGateway _pokemonGateway;
    public PokemonService(IPokemonGateway pokemonGateway)
    {
        _pokemonGateway = pokemonGateway;
    }
    public async Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _pokemonGateway.GetPokemonByIdAsync(id, cancellationToken);
    }
}

//Arquitectura 
//Cliente (postman, insomnia, navegador) -> REST API (PokedexApi) - (Aqui) > PokemonApi (SOAP) -> Base de datos (SQL, MongoDB, etc)