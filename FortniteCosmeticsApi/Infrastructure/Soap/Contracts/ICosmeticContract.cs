using System.ServiceModel;
using FortniteCosmeticsApi.Infrastructure.Soap.Dtos;

namespace FortniteCosmeticsApi.Infrastructure.Soap.Contracts;

[ServiceContract(Name = "FortniteService", Namespace = "http://schemas.fortniteapi.com/fortnite")]
public interface ICosmeticContract
{
    [OperationContract]
    Task<CosmeticResponseDto> GetCosmeticById(Guid id, CancellationToken cancellationToken);
}