using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Utilities;

namespace PokemonApi.Infrastructure.Entities;

public class PokemonEntity
{

    public Guid Id { get; set; } //GUID es un string alfanumerico unico

    public string Name { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public int Health { get; set; }
}