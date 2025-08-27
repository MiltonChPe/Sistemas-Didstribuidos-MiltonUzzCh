using PokemonApi.Dtos;

namespace PokemonApi.Models;

public class Pokemon
{

    public Guid Id { get; set; } //GUID es un string alfanumerico unico

    public string Name { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }

    public Stats Stats { get; set; }
}