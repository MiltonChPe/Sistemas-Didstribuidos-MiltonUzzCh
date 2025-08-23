namespace PokemonApi.Dtos;

using System.Runtime.Serialization;

[DataContract(Name = "CreatePokemonDto", Namespace = "http://pokemon-api/pokemon-service")]

public class CreatePokemonDto
{
    [DataMember(Name = "Name", Order = 1)]
    public string? Name { get; set; }

    [DataMember(Name = "Type", Order = 2)]
    public string? Type { get; set; }

    [DataMember(Name = "Level", Order = 3)]
    public int Level { get; set; }
    
    [DataMember(Name = "Stats", Order = 4)]
    public required StatsDto Stats { get; set; }

    [DataMember(Name = "Health", Order = 5)]
    public int Health { get; set; }
}