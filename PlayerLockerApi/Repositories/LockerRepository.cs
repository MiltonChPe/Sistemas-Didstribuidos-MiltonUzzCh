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

    public async Task<IEnumerable<Locker>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var lockers = await _lockerCollection.Find(filter: t => t.Name.Contains(name)).ToListAsync(cancellationToken);
        return lockers.Select(selector: t => t.ToDomain());
    }

    public async Task<Locker> CreateAsync(Locker locker, CancellationToken cancellationToken)
    {
        var lockerToCreate = locker.ToDocument();
        await _lockerCollection.InsertOneAsync(lockerToCreate, cancellationToken: cancellationToken);
        return lockerToCreate.ToDomain();
    }
}