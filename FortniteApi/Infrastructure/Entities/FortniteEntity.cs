namespace FortniteApi.Infrastructure.Entities;

public class FortniteEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Rarity { get; set; }
    public int Price { get; set; }
    public string Season { get; set; }
    public string Source { get; set; }
}

