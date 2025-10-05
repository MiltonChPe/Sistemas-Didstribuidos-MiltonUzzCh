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

    public async Task<PagedCosmeticResponseDto> GetCosmetics(QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        return await _fortniteRepository.GetCosmeticsAsync(queryParameters, cancellationToken);
    }
    public async Task<IList<CosmeticResponseDto>> GetCosmeticByRarity(string rarity, CancellationToken cancellationToken)
    {
        var cosmetics = await _fortniteRepository.GetCosmeticsByRarityAsync(rarity, cancellationToken);
        return cosmetics.ToResponseDto();
    }

    public async Task<IList<CosmeticResponseDto>> GetCosmeticsByName(string name, CancellationToken cancellationToken)
    {
        var cosmetics = await _fortniteRepository.GetCosmeticsByNameAsync(name, cancellationToken);
        return cosmetics.ToResponseDto();
    }
    public async Task<CosmeticResponseDto> UpdateCosmetic(UpdateCosmeticDto cosmeticupdate, CancellationToken cancellationToken)
    {
        var cosmetic = await _fortniteRepository.GetCosmeticByIdAsync(cosmeticupdate.Id, cancellationToken);
        if (!CosmeticExist(cosmetic))
        {
            throw new FaultException("Cosmetic Not Found");
        }
        if (!await AllowToUpdate(cosmeticupdate, cancellationToken))
        {
            throw new FaultException("Cosmetic with the same Name Already Exist");
        }

        cosmetic.Name = cosmeticupdate.Name;
        cosmetic.Type = cosmeticupdate.Type;
        cosmetic.Rarity = cosmeticupdate.Rarity;

        await _fortniteRepository.UpdateCosmeticAsync(cosmetic, cancellationToken);
        return cosmetic.ToResponseDto();
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

    private static bool CosmeticExist(Cosmetic cosmetic)
    {
        return cosmetic != null;
    }

    private async Task<bool> AllowToUpdate(UpdateCosmeticDto cosmeticupdate, CancellationToken cancellationToken)
    {
        var cosmeticduplicated = await _fortniteRepository.GetByNameAsync(cosmeticupdate.Name, cancellationToken);
        return cosmeticduplicated == null || IsTheSameCosmetic(cosmeticduplicated, cosmeticupdate);
    }
    
    private static bool IsTheSameCosmetic(Cosmetic cosmetic, UpdateCosmeticDto cosmeticupdate)
    {
        return cosmetic.Id == cosmeticupdate.Id;
    }
}