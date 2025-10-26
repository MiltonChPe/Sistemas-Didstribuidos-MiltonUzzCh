using FortniteApi.Models;
using FortniteApi.Dtos;
namespace FortniteApi.Repositories;


public interface IFortniteRepository
{
    Task<Cosmetic> CreateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);

    Task<Cosmetic> GetByNameAsync(string id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Cosmetic>> GetCosmeticsByNameAsync(string name, CancellationToken cancellationToken);
    Task DeleteCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);
    Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);

    Task<IReadOnlyList<Cosmetic>> GetCosmeticsByRarityAsync(string rarity, CancellationToken cancellationToken);

    Task<PagedCosmeticResponseDto> GetCosmeticsAsync(QueryParameters queryParameters, CancellationToken cancellationToken);
}