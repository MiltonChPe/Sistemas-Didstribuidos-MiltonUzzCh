using System.ServiceModel;
using FortniteApi.Dtos;

namespace FortniteApi.Services;

[ServiceContract(Name = "FortniteService", Namespace = "http://schemas.fortniteapi.com/fortnite")]

public interface IFortniteService
{
    [OperationContract]
    Task<CosmeticResponseDto> CreateCosmetic(CreateCosmeticDto cosmetic, CancellationToken cancellationToken);
   
}