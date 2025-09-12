using PokedexApi.Models;

namespace PokedexApi.Gateways;

public interface IPokemonGateway
{
    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);
    
}