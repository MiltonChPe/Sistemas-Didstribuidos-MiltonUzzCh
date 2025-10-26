namespace PokedexApi.Dtos;

public class PatchPokemonRequest
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int? attack { get; set; }
    public int? defense { get; set; }
    public int? speed { get; set; }
    
}