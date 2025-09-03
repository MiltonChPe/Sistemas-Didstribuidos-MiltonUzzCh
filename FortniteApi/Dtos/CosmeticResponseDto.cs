using System.Runtime.Serialization;
namespace FortniteApi.Dtos;

[DataContract(Name = "CosmeticResponseDto", Namespace = "http://schemas.fortniteapi.com/fortnite")]

public class CosmeticResponseDto
{
    [DataMember(Name = "Id", Order = 1)]
    public Guid Id { get; set; }

    [DataMember(Name = "Name", Order = 2)]
    public required string Name { get; set; }

    [DataMember(Name = "Type", Order = 3)]
    public required string Type { get; set; }

    [DataMember(Name = "Rarity", Order = 4)]
    public required string Rarity { get; set; }

    [DataMember(Name = "Price", Order = 5)]
    public int Price { get; set; }

    [DataMember(Name = "Season", Order = 6)]
    public required string Season { get; set; }

    [DataMember(Name = "Source", Order = 7)]
    public required string Source { get; set; }
}