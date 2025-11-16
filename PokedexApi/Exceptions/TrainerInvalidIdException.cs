namespace PokedexApi.Exceptions;

public class TrainerInvalidIdException : Exception
{
    public TrainerInvalidIdException(string id) : base("Invalid trainer id: " + id)
    {
    }
}