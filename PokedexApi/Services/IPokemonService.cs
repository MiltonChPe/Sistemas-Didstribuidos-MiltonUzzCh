namespace PokedexApi.Services;

using PokedexApi.Gateways;
using PokedexApi.Models;
public interface IPokemonService
{
    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);

    Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken);

    Task<PagedResult<Pokemon>> GetPokemonsAsync(string name, string type, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken);

    Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);

    Task<Pokemon> PatchPokemonAsync(Guid id, string? name, string? type, int? attack, int? defense, int? speed, CancellationToken cancellationToken);
}