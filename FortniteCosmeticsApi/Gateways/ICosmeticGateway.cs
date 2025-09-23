namespace FortniteCosmeticsApi.Gateways;
using FortniteCosmeticsApi.Models;
public interface ICosmeticGateway
{
    Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken);
}