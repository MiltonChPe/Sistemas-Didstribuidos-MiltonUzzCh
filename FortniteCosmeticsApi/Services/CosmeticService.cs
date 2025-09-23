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

}