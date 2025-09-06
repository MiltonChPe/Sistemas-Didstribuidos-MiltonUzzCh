using System.Runtime.Serialization;

namespace FortniteApi.Dtos;

[DataContract(Name = "UpdateCosmeticDto", Namespace = "http://schemas.fortniteapi.com/fortnite")]
public class UpdateCosmeticDto
{
    [DataMember(Name = "id", Order = 1)]
    public Guid Id { get; set; }

    [DataMember(Name = "name", Order = 2)]
    public string Name { get; set; }

    [DataMember(Name = "type", Order = 3)]
    public string Type { get; set; }
    
    [DataMember(Name = "rarity", Order = 4)]
    public string Rarity { get; set; }
    
}