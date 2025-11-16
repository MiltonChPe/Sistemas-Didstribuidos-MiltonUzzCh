namespace PokedexApi.Exceptions;

public class TrainerAlreadyExistsException : Exception
{
    public TrainerAlreadyExistsException(string name) : base($"Trainer with same name '{name}' already exists.")
    {
    }
}