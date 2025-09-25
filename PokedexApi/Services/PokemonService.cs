namespace PokedexApi.Services;

using PokedexApi.Models;
using PokedexApi.Gateways;
using PokedexApi.Mappers;
using PokedexApi.Exceptions;
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
  
  public async Task<PagedResult<Pokemon>> GetPokemonsAsync(string name, string type, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken)
    {
        return await _pokemonGateway.GetPokemonsAsync(name, type, pageSize, pageNumber, orderBy, orderDirection, cancellationToken);
    }

    public async Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken)
    {
        await _pokemonGateway.DeletePokemonAsync(id, cancellationToken);
        
    }

    public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        //validar que no exista otro pokemon con el mismo nombre
        //con peticion al pokemonApi que esta en SOAP 
        //usar endpoint GetPokemonsByName
        var pokemons = await _pokemonGateway.GetPokemonsByNameAsync(pokemon.Name, cancellationToken);

        if (PokemonExists(pokemons, pokemon.Name))
        {
            throw new PokemonAlreadyExistsException(pokemon.Name);
        }
        //COMO QUITO PARA SOLO QUEDARME CON LA VALIDACION DEL OTRO LADO



        return await _pokemonGateway.CreatePokemonAsync(pokemon, cancellationToken);

    }

    public static bool PokemonExists(IList<Pokemon> pokemons, string pokemonNameToSearch)
    {
        return pokemons.Any(p => p.Name.ToLower().Equals(pokemonNameToSearch.ToLower()));
    }
}

//Arquitectura 
//Cliente (postman, insomnia, navegador) -> REST API (PokedexApi) - (Aqui) > PokemonApi (SOAP) -> Base de datos (SQL, MongoDB, etc)