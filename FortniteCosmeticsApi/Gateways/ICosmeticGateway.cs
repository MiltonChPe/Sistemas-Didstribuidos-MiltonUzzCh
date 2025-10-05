namespace FortniteCosmeticsApi.Gateways;
using FortniteCosmeticsApi.Models;
public interface ICosmeticGateway
{
    Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IList<Cosmetic>> GetCosmeticsByNameAsync(string name, CancellationToken cancellationToken);
    Task<Cosmetic> CreateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);

    Task<PagedResult<Cosmetic>> GetCosmeticsAsync(string name, string type, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken);
    Task DeleteCosmeticAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken);

}