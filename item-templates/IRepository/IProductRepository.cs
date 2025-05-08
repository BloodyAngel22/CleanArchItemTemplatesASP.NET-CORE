using Template.Core.Entities;

namespace Template.Core.IRepositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task AddProductAsync(Product entity);
    Task UpdateProductAsync(Product entity);
    Task DeleteProductAsync(Guid id);
}