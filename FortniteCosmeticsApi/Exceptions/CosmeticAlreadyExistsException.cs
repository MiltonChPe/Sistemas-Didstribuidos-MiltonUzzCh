namespace FortniteCosmeticsApi.Exceptions;

public class CosmeticAlreadyExistsException : Exception
{
    public CosmeticAlreadyExistsException(string cosmeticName) : base($"A cosmetic with the name '{cosmeticName}' already exists.")
    {
    }
}