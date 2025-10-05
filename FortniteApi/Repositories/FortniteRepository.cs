using Microsoft.EntityFrameworkCore;
using FortniteApi.Infrastructure;
using FortniteApi.Models;
using FortniteApi.Mappers;
using FortniteApi.Dtos;

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

    public async Task<IReadOnlyList<Cosmetic>> GetCosmeticsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var cosmetics = await _context.Cosmetics.AsNoTracking().Where(s => s.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken);
        return cosmetics.ToModel();
    }

    public async Task<PagedCosmeticResponseDto> GetCosmeticsAsync(QueryParameters queryParameters, CancellationToken cancellationToken)
    {

        IQueryable<Infrastructure.Entities.FortniteEntity> query = _context.Cosmetics.AsNoTracking();

        if (!string.IsNullOrEmpty(queryParameters.Name))
        {
            query = query.Where(s => s.Name.ToLower().Contains(queryParameters.Name.ToLower()));
        }

        if (!string.IsNullOrEmpty(queryParameters.Type))
        {
            query = query.Where(s => s.Type.ToLower() == queryParameters.Type.ToLower());
        }

        var OrderByField = queryParameters.OrderBy.ToLower();
        var isAcending = queryParameters.OrderDirection.ToLower() == "asc";

        if (OrderByField.Contains("Name"))
        {
            query = isAcending ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name);
        }
        else if (OrderByField.Contains("Type"))
        {
            query = isAcending ? query.OrderBy(s => s.Type) : query.OrderByDescending(s => s.Type);
        }
        else if (OrderByField.Contains("Rarity"))
        {
            query = isAcending ? query.OrderBy(s => s.Rarity) : query.OrderByDescending(s => s.Rarity);
        }
        else
        {
            query = isAcending ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name);
        }

        var totalCosmetics = await query.CountAsync(cancellationToken);

        var paginacion = await query.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize)
            .ToListAsync(cancellationToken);

        var cosmeticModels = paginacion.ToModel();
        var cosmeticsDtos = cosmeticModels.ToResponseDto();

        return new PagedCosmeticResponseDto
        {
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            TotalRecords = totalCosmetics,
            TotalPages = (int)Math.Ceiling(totalCosmetics / (double)queryParameters.PageSize),
            Data = cosmeticsDtos.ToList()
        };
    }
}