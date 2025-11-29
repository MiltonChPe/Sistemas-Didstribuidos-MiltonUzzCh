using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PlayerLockerApi.Infrastructure;
using PlayerLockerApi.Infrastructure.Documents;
using PlayerLockerApi.Mappers;
using PlayerLockerApi.Models;

namespace PlayerLockerApi.Repositories;

public class LockerRepository : ILockerRepository
{
    private readonly IMongoCollection<LockerDocument> _lockerCollection;

    public LockerRepository(IMongoDatabase database, IOptions<MongoDBSettings> settings)
    {
        _lockerCollection = database.GetCollection<LockerDocument>(settings.Value.LockerCollectionName);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken){
        await _lockerCollection.DeleteOneAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Locker>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var lockers = await _lockerCollection.Find(filter: t => t.Name.Contains(name)).ToListAsync(cancellationToken);
        return lockers.Select(selector: t => t.ToDomain());
    }

    public async Task<Locker?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var locker = await _lockerCollection.Find(l => l.Id == id).FirstOrDefaultAsync(cancellationToken);
        return locker?.ToDomain();
    }

    public async Task<Locker> CreateAsync(Locker locker, CancellationToken cancellationToken)
    {
        var lockerToCreate = locker.ToDocument();
        await _lockerCollection.InsertOneAsync(lockerToCreate, cancellationToken: cancellationToken);
        return lockerToCreate.ToDomain();
    }

    public async Task UpdateAsync(Locker locker, CancellationToken cancellationToken)
    {
       var update = Builders<LockerDocument>.Update
            .Set(l => l.Name, locker.Name)
            .Set(l => l.Skin, locker.Skin)
            .Set(l => l.Backblings, locker.Backblings)
            .Set(l => l.Pickaxe, locker.Pickaxe)
            .Set(l => l.Glider, locker.Glider)
            .Set(l => l.Contrail, locker.Contrail)
            .Set(l => l.Emote, locker.Emote);

        await _lockerCollection.UpdateOneAsync(l => l.Id == locker.Id, update, cancellationToken: cancellationToken);
    }
}