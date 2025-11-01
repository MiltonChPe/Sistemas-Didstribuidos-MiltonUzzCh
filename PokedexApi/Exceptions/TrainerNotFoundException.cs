namespace PokedexApi.Exceptions;

public class TrainerNotFoundException : Exception
{
    public TrainerNotFoundException(string Id) : base ($"Trainer with id {Id} not found.")
    {
    }

}