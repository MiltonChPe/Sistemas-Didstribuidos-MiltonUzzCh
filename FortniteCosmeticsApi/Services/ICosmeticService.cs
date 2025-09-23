namespace FortniteCosmeticsApi.Services;
using FortniteCosmeticsApi.Models;

public interface ICosmeticService
{ 
    Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken);
}