using PokemonApi.Models;

namespace PokemonApi.Repositories;

public interface IPokemonRepository
{
    Task<Pokemon> GetByNameAsync(string id, CancellationToken cancellationToken);

    Task<Pokemon> CreateAsync(Pokemon pokemon, CancellationToken cancellationToken);
}