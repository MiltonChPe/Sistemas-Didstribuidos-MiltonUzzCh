using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrainerApi.Infrastructure;
using TrainerApi.Infrastructure.Documents;
using TrainerApi.Models;
using TrainerApi.Mappers;

namespace TrainerApi.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly IMongoCollection<TrainerDocument> _trainerCollection;

    public TrainerRepository(IMongoDatabase database, IOptions<MongoDBSettings> settings)
    {
        _trainerCollection = database.GetCollection<TrainerDocument>(settings.Value.TrainerCollectionName);

    }

    public async Task<Trainer?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var trainer = await _trainerCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
        return trainer?.ToDomain();
    }

    public async Task<Trainer> CreateAsync(Trainer trainer, CancellationToken cancellationToken)
    {
        trainer.CreatedAt = DateTime.UtcNow;
        var trainerToCreate = trainer.ToDocument();
        await _trainerCollection.InsertOneAsync(trainerToCreate, cancellationToken: cancellationToken);
        return trainerToCreate.ToDomain();
    }

    public async Task UpdateAsync(Trainer trainer, CancellationToken cancellationToken)
    {
        var update = Builders<TrainerDocument>.Update
            .Set(t => t.Name, trainer.Name)
            .Set(t => t.Age, trainer.Age)
            .Set(t => t.Birthdate, trainer.Birthdate)
            .Set(t => t.Medals, trainer.Medals.Select(m => m.ToDocument()).ToList());

        await _trainerCollection.UpdateOneAsync(t => t.Id == trainer.Id, update, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _trainerCollection.DeleteOneAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Trainer>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var trainers = await _trainerCollection.Find(filter: t => t.Name.Contains(name)).ToListAsync(cancellationToken);
        return trainers.Select(selector: t => t.ToDomain());
    }
}