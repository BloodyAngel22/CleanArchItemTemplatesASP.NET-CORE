using Microsoft.EntityFrameworkCore;
using Template.Core.Entities;
using Template.Core.IRepositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Product>> GetProductsAsync(
        CancellationToken cancellationToken = default
    )
    {
        var entities = await _context.Products.ToListAsync(cancellationToken);
        return entities;
    }

    public async Task<IEnumerable<Product>> GetProductsWithPaginationAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        var entities = await _context
            .Products.AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return entities;
    }

    public async Task<Product> GetProductByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var entity =
            await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new Exception("Product not found");

        return entity;
    }

    public async Task AddProductAsync(Product entity, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductAsync(
        Product entity,
        CancellationToken cancellationToken = default
    )
    {
        var entityToUpdate =
            await _context.Products.FindAsync(entity.Id, cancellationToken)
            ?? throw new Exception("Product not found");

        entityToUpdate.Name = entity.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity =
            await _context.Products.FindAsync(id, cancellationToken)
            ?? throw new Exception("Product not found");

        _context.Products.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
