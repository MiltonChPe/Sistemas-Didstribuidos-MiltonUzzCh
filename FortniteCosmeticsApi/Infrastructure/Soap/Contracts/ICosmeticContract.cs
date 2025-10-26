using System.ServiceModel;
using FortniteCosmeticsApi.Infrastructure.Soap.Dtos;

namespace FortniteCosmeticsApi.Infrastructure.Soap.Contracts;

[ServiceContract(Name = "FortniteService", Namespace = "http://schemas.fortniteapi.com/fortnite")]
public interface ICosmeticContract
{
    [OperationContract]
    Task<CosmeticResponseDto> CreateCosmetic(CreateCosmeticDto cosmetic, CancellationToken cancellationToken);

    [OperationContract]
    Task<DeleteCosmeticResponseDto> DeleteCosmetic(Guid id, CancellationToken cancellationToken);

    [OperationContract]
    Task<IList<CosmeticResponseDto>> GetCosmeticsByName(string name, CancellationToken cancellationToken);

    [OperationContract]
    Task<CosmeticResponseDto> GetCosmeticById(Guid id, CancellationToken cancellationToken);

    [OperationContract]
    Task<CosmeticResponseDto> UpdateCosmetic(UpdateCosmeticDto cosmetic, CancellationToken cancellationToken);

    [OperationContract]
    Task<IList<CosmeticResponseDto>> GetCosmeticByRarity(string rarity, CancellationToken cancellationToken);

    [OperationContract]
    Task<PagedCosmeticResponseDto> GetCosmetics(QueryParameters queryParameters, CancellationToken cancellationToken);
}