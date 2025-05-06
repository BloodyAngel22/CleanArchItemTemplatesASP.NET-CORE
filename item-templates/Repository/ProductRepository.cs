using Template.Core.IRepositories;
using Template.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Template.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var entities = await _context.Products.AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var entity = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Product not found");

        return entity;
    }

    public async Task AddProductAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product entity)
    {
        var entityToUpdate = await _context.Products.FindAsync(entity.Id) ?? throw new Exception("Product not found");

        entityToUpdate.Name = entity.Name;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var entity = await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");

        _context.Products.Remove(entity);

        await _context.SaveChangesAsync();
    }
}