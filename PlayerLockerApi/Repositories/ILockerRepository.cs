using PlayerLockerApi.Models;

namespace PlayerLockerApi.Repositories;

public interface ILockerRepository
{
    Task<Locker> CreateAsync(Locker locker, CancellationToken cancellationToken);
    Task<IEnumerable<Locker>> GetByNameAsync(string name, CancellationToken cancellationToken);
}