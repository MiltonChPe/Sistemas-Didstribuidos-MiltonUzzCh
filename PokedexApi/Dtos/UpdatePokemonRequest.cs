using PokedexApi.Models;

namespace PokedexApi.Dtos;

public class UpdatePokemonRequest
{
    public string Name { get; set; }
    public string Type { get; set; }
    public StatsRquest Stats { get; set; }
}