using PlayerLockerApi.Models;

namespace PlayerLockerApi.Repositories;

public interface ILockerRepository
{
    Task<Locker> CreateAsync(Locker locker, CancellationToken cancellationToken);
    Task<IEnumerable<Locker>> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task<Locker?> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task UpdateAsync(Locker locker, CancellationToken cancellationToken);
}