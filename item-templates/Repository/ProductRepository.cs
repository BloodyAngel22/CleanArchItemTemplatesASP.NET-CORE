using Template.Core.IRepositories;
using Template.Core.Entities;
using Template.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Template.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
        return entities;
    }

    public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new Exception("Product not found");

        return entity;
    }

    public async Task AddProductAsync(Product entity, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductAsync(Product entity, CancellationToken cancellationToken = default)
    {
        var entityToUpdate = await _context.Products.FindAsync(entity.Id, cancellationToken) ?? throw new Exception("Product not found");

        entityToUpdate.Name = entity.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Products.FindAsync(id, cancellationToken) ?? throw new Exception("Product not found");

        _context.Products.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}