using System.ServiceModel;
using FortniteApi.Dtos;
using FortniteApi.Infrastructure;
using FortniteApi.Mappers;
using FortniteApi.Models;
using FortniteApi.Repositories;
using FortniteApi.Validators;
using Mysqlx.Crud;

namespace FortniteApi.Services;

public class FortniteService : IFortniteService
{
    private readonly IFortniteRepository _fortniteRepository;

    public FortniteService(IFortniteRepository fortniteRepository)
    {
        _fortniteRepository = fortniteRepository;
    }

    public async Task<DeleteCosmeticResponseDto> DeleteCosmetic(Guid id, CancellationToken cancellationToken)
    {
        var cosmetic = await _fortniteRepository.GetCosmeticByIdAsync(id, cancellationToken);
        if (!CosmeticExist(cosmetic))
        {
            throw new FaultException("Cosmetic Not Found");
        }

        await _fortniteRepository.DeleteCosmeticAsync(cosmetic, cancellationToken);
        return new DeleteCosmeticResponseDto { Success = true };
    }

    public async Task<CosmeticResponseDto> GetCosmeticById(Guid id, CancellationToken cancellationToken)
    {
        var cosmetic = await _fortniteRepository.GetCosmeticByIdAsync(id, cancellationToken);
        return CosmeticExist(cosmetic) ? cosmetic.ToResponseDto() : throw new FaultException("Cosmetic Not Found");

    }
    public async Task<CosmeticResponseDto> CreateCosmetic(CreateCosmeticDto cosmeticRequest, CancellationToken cancellationToken)
    {
        cosmeticRequest.ValidateName().ValidateType().ValidateRarity()
            .ValidatePrice().ValidateSeason().ValidateSource();

        if (await CosmeticAlreadyExist(cosmeticRequest.Name, cancellationToken))
        {
            throw new FaultException("Cosmetic Already Exist");
        }

        var cosmetic = await _fortniteRepository.CreateCosmeticAsync(cosmeticRequest.ToModel(), cancellationToken);
        return cosmetic.ToResponseDto();
    }

    private async Task<bool> CosmeticAlreadyExist(string name, CancellationToken cancellationToken)
    {
        var cosmetic = await _fortniteRepository.GetByNameAsync(name, cancellationToken);
        return cosmetic != null;
    }

    private static bool CosmeticExist(Cosmetic cosmetic) {
        return cosmetic != null; 
    }
}