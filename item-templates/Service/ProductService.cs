using Template.Application.DTOs.Request;
using Template.Application.Helpers;
using Template.Core.Entities;
using Template.Core.Exceptions;
using Template.Core.IRepositories;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace Template.Application.Services;

public class ProductService(
    IProductRepository productRepository,
    IValidator<ProductDTORequest> productValidator,
    ILogger<ProductService> logger
    )
{
    private readonly IProductRepository _productRepository = productRepository;

    private readonly IValidator<ProductDTORequest> _productValidator = productValidator;

    private readonly ILogger<ProductService> _logger = logger;

    public async Task<ServiceResult<IEnumerable<Product>>> GetProducts()
    {
        try
        {
            var products = await _productRepository.GetProductsAsync();
            return ServiceResult<IEnumerable<Product>>.Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResult<IEnumerable<Product>>.Fail();
        }
    }

    public async Task<ServiceResult<Product>> GetProduct(Guid id)
    {
        try
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return ServiceResult<Product>.Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResult<Product>.Fail();
        }
    }

    public async Task<ServiceResult<string>> AddProduct(ProductDTORequest entity)
    {
        try
        {
            var validationResult = await _productValidator.ValidateAsync(entity);

            if (!validationResult.IsValid)
                throw new ValidateException(ValidatorError.GetErrorMessages(ValidatorError.GetErrors(validationResult)));

                var entityToAdd = new Product
                {
                    Name = entity.Name
                };

            await _productRepository.AddProductAsync(entityToAdd);
            return ServiceResult<string>.Ok("Product created successfully");
        }
        catch (ValidateException ex)
        {
            _logger.LogError($"Validation Error/s: {ex.Message}");
            return ServiceResult<string>.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResult<string>.Fail();
        }
    }

    public async Task<ServiceResult<string>> UpdateProduct(Guid id, ProductDTORequest entity)
    {
        try
        {
            var validationResult = await _productValidator.ValidateAsync(entity);

            if (!validationResult.IsValid)
                throw new ValidateException(ValidatorError.GetErrorMessages(ValidatorError.GetErrors(validationResult)));

            var entityToUpdate = new Product
            {
                Id = id,
                Name = entity.Name
            };

            await _productRepository.UpdateProductAsync(entityToUpdate);
            return ServiceResult<string>.Ok("Product updated successfully");
        }
        catch (ValidateException ex)
        {
            _logger.LogError($"Validation Error/s: {ex.Message}");
            return ServiceResult<string>.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResult<string>.Fail();
        }
    }

    public async Task<ServiceResult<string>> DeleteProduct(Guid id)
    {
        try
        {
            await _productRepository.DeleteProductAsync(id);
            return ServiceResult<string>.Ok("Product deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResult<string>.Fail();
        }
    }
}