using FortniteApi.Models;
namespace FortniteApi.Repositories;

public interface IFortniteRepository
{
    Task<Cosmetic> CreateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);

    Task<Cosmetic> GetByNameAsync(string id, CancellationToken cancellationToken);

    Task DeleteCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);
    Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);
    
    Task<IReadOnlyList<Cosmetic>> GetCOsmeticsByRarityAsync(string rarity, CancellationToken cancellationToken);
}