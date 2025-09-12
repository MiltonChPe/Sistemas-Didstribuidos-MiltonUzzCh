using PokedexApi.Models;

namespace PokedexApi.Gateways;

public interface IPokemonGateway
{
    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken);
    
    Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
}