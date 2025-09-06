using FortniteApi.Models;
namespace FortniteApi.Repositories;

public interface IFortniteRepository
{
    Task<Cosmetic> CreateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);

    Task<Cosmetic> GetByNameAsync(string id, CancellationToken cancellationToken);
    
    Task DeleteCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);
    Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken);
}