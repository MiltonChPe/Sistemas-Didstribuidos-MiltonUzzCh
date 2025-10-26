using System.Runtime.Serialization;

namespace FortniteCosmeticsApi.Infrastructure.Soap.Dtos;


[DataContract(Name = "PagedCosmeticResponseDto", Namespace = "http://schemas.fortniteapi.com/fortnite")]
public class PagedCosmeticResponseDto
{
    [DataMember(Order = 1)]
    public int PageNumber { get; set; }

    [DataMember(Order = 2)]
    public int PageSize { get; set; }

    [DataMember(Order = 3)]
    public int TotalRecords { get; set; }

    [DataMember(Order = 4)]
    public int TotalPages { get; set; }

    [DataMember(Order = 5)]
    public List<CosmeticResponseDto> Data { get; set; }
}