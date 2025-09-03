using System.ServiceModel;
using FortniteApi.Dtos;
using FortniteApi.Infrastructure;
using FortniteApi.Mappers;
using FortniteApi.Repositories;
using FortniteApi.Validators;

namespace FortniteApi.Services;

public class FortniteService : IFortniteService
{
    private readonly IFortniteRepository _fortniteRepository;

    public FortniteService(IFortniteRepository fortniteRepository)
    {
        _fortniteRepository = fortniteRepository;
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
}