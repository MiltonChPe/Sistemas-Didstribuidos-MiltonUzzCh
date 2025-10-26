namespace TrainerApi.Models;

public class Trainer
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public DateTime Birthdate { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<Medal> Medals { get; set; } = new();
}

public class Medal
{
    public string Region { get; set; } = null!;
    public MedalType Type { get; set; }

}
public enum MedalType
{
    Unknown = 0,
    Gold = 1,
    Silver = 2,
    Bronze = 3
}