using System.ServiceModel;
using PokedexApi.Models;
using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Mappers;
using PokedexApi.Exceptions;
using System.Diagnostics.Contracts;

namespace PokedexApi.Gateways;

public class PokemonGateway : IPokemonGateway
{
    private readonly IPokemonContract _pokemonContract;
    private readonly ILogger<PokemonGateway> _logger;
    public PokemonGateway(IConfiguration configuration, ILogger<PokemonGateway> logger)
    {
        var binding = new BasicHttpBinding();
        var endpoint = new EndpointAddress(configuration.GetValue<string>("PokemonService:Url"));
        _pokemonContract = new ChannelFactory<IPokemonContract>(binding, endpoint).CreateChannel();
        _logger = logger;
    }


    public async Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var pokemon = await _pokemonContract.GetPokemonById(id, cancellationToken);
            return pokemon.ToModel();
        }
        catch (FaultException ex) when (ex.Message == "El pokemon no existe")
        {
            return null;
        }
    }

    public async Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        try
        {
            await _pokemonContract.UpdatePokemon(pokemon.ToUpdateRequest(), cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Pokemon Not found")
        {
            throw new PokemonNotFoundException(pokemon.Id);
        }
        catch (FaultException ex) when (ex.Message == "Pokemon with this name alrwady exist")
        {
            throw new PokemonAlreadyExistsException(pokemon.Name);
        }
    }
    
    public async Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _pokemonContract.DeletePokemon(id, cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Pokemon not fuound")
        {
            _logger.LogWarning(ex, "Pokemon not fuound");
            throw new PokemonNotFoundException(id);
        }
    }

      public async Task<PagedResult<Pokemon>> GetPokemonsAsync(string name, string type, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken)
    {
    
        var queryParameters = new Infrastructure.Soap.Dtos.QueryParameters
        {
            Name = name,
            Type = type,
            PageSize = pageSize,
            PageNumber = pageNumber,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        };

        var paginated = await _pokemonContract.GetPokemons(queryParameters, cancellationToken);

        return paginated.ToPagedResult();
    }

    public async Task<IList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken)
    {
        _logger.LogInformation(":()");
        var pokemons = await _pokemonContract.GetPokemonsByName(name, cancellationToken);
        return pokemons.ToModel();
    }

    public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("sending request to SOAP API, with name: {Name}", pokemon.Name);
            var createdPokemon = await _pokemonContract.CreatePokemon(pokemon.ToRequest(), cancellationToken);
            return createdPokemon.ToModel();
        }

        catch (Exception ex)
        { 
            _logger.LogError(ex, "Algo trono en el create pokemon a soap");
            throw;
        }


        //LOGS
        //Fatal
        //Error
        //Warn
        //information 
        //Debug
        //Trace
    }
}