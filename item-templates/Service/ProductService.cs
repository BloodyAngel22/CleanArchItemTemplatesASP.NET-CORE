using Template.Application.Helpers;
using Template.Core.IRepositories;
using Template.Core.Models;

namespace Template.Application.Services;

public class ProductService(
    IProductRepository productRepository,
    ILogger<ProductService> logger
    )
{
    private readonly IProductRepository _productRepository = productRepository;

    private readonly ILogger<ProductService> _logger = logger;

    public async Task<ServiceResultHelper<IEnumerable<Product>>> GetProducts()
    {
        try
        {
            return ServiceResultHelper<IEnumerable<Product>>.Ok(await _productRepository.GetProductsAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResultHelper<IEnumerable<Product>>.Fail();
        }
    }

    public async Task<ServiceResultHelper<Product>> GetProduct(Guid id)
    {
        try
        {
            return ServiceResultHelper<Product>.Ok(await _productRepository.GetProductByIdAsync(id));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResultHelper<Product>.Fail();
        }
    }

    public async Task<ServiceResultHelper<string>> AddProduct(ProductDTO entity)
    {
        try
        {
            var entityToAdd = new Product
            {
                Name = entity.Name
            };

            await _productRepository.AddProductAsync(entityToAdd);
            return ServiceResultHelper<string>.Ok("Product created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResultHelper<Product>.Fail();
        }
    }

    public async Task<ServiceResultHelper<string>> UpdateProduct(Guid id, ProductDTO entity)
    {
        try
        {
            var entityToUpdate = new Product
            {
                Id = id,
                Name = entity.Name
            };

            await _productRepository.UpdateProductAsync(entityToUpdate);
            return ServiceResultHelper<string>.Ok("Product updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResultHelper<Product>.Fail();
        }
    }

    public async Task<ServiceResultHelper<string>> DeleteProduct(Guid id)
    {
        try
        {
            await _productRepository.DeleteProductAsync(id);
            return ServiceResultHelper<string>.Ok("Product deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResultHelper<Product>.Fail();
        }
    }
}