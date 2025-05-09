using FluentValidation;
using Microsoft.Extensions.Logging;
using Template.Application.DTOs.Request;
using Template.Application.Helpers;
using Template.Core.Entities;
using Template.Core.Exceptions;
using Template.Core.IRepositories;

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

    public async Task<ServiceResult<IEnumerable<Product>>> GetProducts(
        PaginationDTORequest pagination,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var validationResult = await _productValidator.ValidateAsync(
                pagination,
                cancellationToken
            );

            if (!validationResult.IsValid)
                throw new ValidateException(
                    ValidatorError.GetErrorMessages(ValidatorError.GetErrors(validationResult))
                );

            var products = await _productRepository.GetProductsWithPaginationAsync(
                pagination,
                cancellationToken
            );
            return ServiceResult<IEnumerable<Product>>.Ok(products);
        }
        catch (ValidateException ex)
        {
            _logger.LogError($"Validation Error/s: {ex.Message}");
            return ServiceResult<IEnumerable<Product>>.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResult<IEnumerable<Product>>.Fail();
        }
    }

    public async Task<ServiceResult<Product>> GetProduct(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            return ServiceResult<Product>.Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResult<Product>.Fail();
        }
    }

    public async Task<ServiceResult<string>> AddProduct(
        ProductDTORequest entity,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var validationResult = await _productValidator.ValidateAsync(entity, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidateException(
                    ValidatorError.GetErrorMessages(ValidatorError.GetErrors(validationResult))
                );

            var entityToAdd = new Product { Name = entity.Name };

            await _productRepository.AddProductAsync(entityToAdd, cancellationToken);
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

    public async Task<ServiceResult<string>> UpdateProduct(
        Guid id,
        ProductDTORequest entity,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var validationResult = await _productValidator.ValidateAsync(entity, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidateException(
                    ValidatorError.GetErrorMessages(ValidatorError.GetErrors(validationResult))
                );

            var entityToUpdate = new Product { Id = id, Name = entity.Name };

            await _productRepository.UpdateProductAsync(entityToUpdate, cancellationToken);
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

    public async Task<ServiceResult<string>> DeleteProduct(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await _productRepository.DeleteProductAsync(id, cancellationToken);
            return ServiceResult<string>.Ok("Product deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return ServiceResult<string>.Fail();
        }
    }
}
