namespace FortniteCosmeticsApi.Services;
using FortniteCosmeticsApi.Models;

public interface ICosmeticService
{
    Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Cosmetic> CreateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);

    Task<PagedResult<Cosmetic>> GetCosmeticsAsync(string name, string type, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken);

    Task DeleteCosmeticAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);
    
    Task<Cosmetic> PatchCosmeticAsync(Guid id, string? name, string? type, string? rarity, CancellationToken cancellationToken);
}