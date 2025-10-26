using System.ComponentModel.DataAnnotations;

namespace FortniteCosmeticsApi.Dtos;

public class CreateCosmeticRequest
{   
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public string Rarity { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public string Season { get; set; }
    [Required]
    public string Source { get; set; }

}