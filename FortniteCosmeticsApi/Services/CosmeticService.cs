using FortniteCosmeticsApi.Exceptions;
using FortniteCosmeticsApi.Gateways;
using FortniteCosmeticsApi.Models;

namespace FortniteCosmeticsApi.Services;

public class CosmeticService : ICosmeticService
{
    private readonly ICosmeticGateway _cosmeticGateway;
    public CosmeticService(ICosmeticGateway cosmeticGateway)
    {
        _cosmeticGateway = cosmeticGateway;
    }

    public async Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _cosmeticGateway.GetCosmeticByIdAsync(id, cancellationToken);
    }

    public async Task<Cosmetic> CreateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken)
    {
        var cosmetics = await _cosmeticGateway.GetCosmeticsByNameAsync(cosmetic.Name, cancellationToken);
        if (CosmeticExists(cosmetics, cosmetic.Name))
        {
            throw new CosmeticAlreadyExistsException(cosmetic.Name);
        }

        return await _cosmeticGateway.CreateCosmeticAsync(cosmetic, cancellationToken);
    }

    public static bool CosmeticExists(IList<Cosmetic> cosmetics, string name)
    {
        return cosmetics.Any(s => s.Name.ToLower().Equals(name.ToLower()));
    }

    public async Task<PagedResult<Cosmetic>> GetCosmeticsAsync(string name, string type, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken)
    {
        return await _cosmeticGateway.GetCosmeticsAsync(name, type, pageSize, pageNumber, orderBy, orderDirection, cancellationToken);
    }

    public async Task DeleteCosmeticAsync(Guid id, CancellationToken cancellationToken)
    {
        await _cosmeticGateway.DeleteCosmeticAsync(id, cancellationToken);
    }

    public async Task UpdateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken)
    {
        await _cosmeticGateway.UpdateCosmeticAsync(cosmetic, cancellationToken);
    }
    
    public async Task<Cosmetic> PatchCosmeticAsync(Guid id, string? name, string? type, string? rarity, CancellationToken cancellationToken)
    {
        var cosmetic = await _cosmeticGateway.GetCosmeticByIdAsync(id, cancellationToken);
        if (cosmetic == null)
        {
            throw new CosmeticNotFoundException(id);
        }

        cosmetic.Name = name ?? cosmetic.Name;
        cosmetic.Type = type ?? cosmetic.Type;
        cosmetic.Rarity = rarity ?? cosmetic.Rarity;

        await _cosmeticGateway.UpdateCosmeticAsync(cosmetic, cancellationToken);
        return cosmetic;
    }

}