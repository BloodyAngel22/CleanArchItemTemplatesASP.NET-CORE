using Template.Core.Entities;

namespace Template.Core.IRepositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
    Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddProductAsync(Product entity, CancellationToken cancellationToken = default);
    Task UpdateProductAsync(Product entity, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);
}