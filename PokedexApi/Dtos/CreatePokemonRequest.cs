using System.ComponentModel.DataAnnotations;

namespace PokedexApi.Dtos;

public class CreatePokemonRequest
{
    [Required]
    public string Name { get; set; }
    public int Level { get; set; }

    [MinLength(3)]
    public string Type { get; set; }
    public StatsRquest Stats { get; set; } 
}

public class StatsRquest
{
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
}