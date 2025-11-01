namespace PokedexApi.Services;

public interface ITrainerService
{
    Task<Models.Trainer> GetByIdAsync(string id, CancellationToken cancellationToken);
}