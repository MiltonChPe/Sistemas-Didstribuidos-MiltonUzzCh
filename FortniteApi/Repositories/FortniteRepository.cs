using Microsoft.EntityFrameworkCore;
using FortniteApi.Infrastructure;
using FortniteApi.Models;
using FortniteApi.Mappers;

namespace FortniteApi.Repositories;

public class FortniteRepository : IFortniteRepository
{
    private readonly RelationalDbContext _context;

    public FortniteRepository(RelationalDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Cosmetic>> GetCosmeticsByRarityAsync(string rarity, CancellationToken cancellationToken)
    {
        var cosmetics = await _context.Cosmetics.AsNoTracking().Where(s => s.Rarity == rarity).ToListAsync(cancellationToken);
        return cosmetics.ToModel();
    }
    public async Task UpdateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken)
    {
        _context.Cosmetics.Update(cosmetic.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken)
    {
        _context.Cosmetics.Remove(cosmetic.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<Cosmetic> CreateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken)
    {
        var cosmeticToCreate = cosmetic.ToEntity();
        cosmeticToCreate.Id = Guid.NewGuid();
        await _context.Cosmetics.AddAsync(cosmeticToCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cosmeticToCreate.ToModel();
    }

    public async Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cosmetic = await _context.Cosmetics.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return cosmetic.ToModel();
    }
    public async Task<Cosmetic> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var cosmetic = await _context.Cosmetics.AsNoTracking().FirstOrDefaultAsync(s => s.Name.Contains(name));
        return cosmetic.ToModel();
    }
}