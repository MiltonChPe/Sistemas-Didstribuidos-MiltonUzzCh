namespace FortniteApi.Dtos;

using System.Runtime.Serialization;

[DataContract(Name = "CreateCosmeticDto", Namespace = "http://schemas.fortniteapi.com/fortnite")]

public class CreateCosmeticDto
{
    [DataMember(Name = "Name",Order = 1)]
    public string? Name { get; set; }

    [DataMember(Name = "Type",Order = 2)]
    public string? Type { get; set; }

    [DataMember(Name="Rarity", Order = 3)]
    public string? Rarity { get; set; }

    [DataMember(Name= "Price", Order = 4)]
    public int Price { get; set; }

    [DataMember(Name = "Season", Order = 5)]
    public string? Season { get; set; }

    [DataMember(Name = "Source", Order = 6)]
    public string? Source { get; set; }
}