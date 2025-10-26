using System.Runtime.Serialization;
namespace FortniteCosmeticsApi.Infrastructure.Soap.Dtos;
[DataContract(Name = "DeleteCosmeticResponseDto", Namespace = "http://schemas.fortniteapi.com/fortnite")]
public class DeleteCosmeticResponseDto
{
    [DataMember(Name = "Success", Order = 1)]
    public bool Success { get; set; }
}