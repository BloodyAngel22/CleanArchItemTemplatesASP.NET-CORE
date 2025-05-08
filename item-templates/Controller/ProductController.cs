using Template.Application.DTOs.Request;
using Template.Application.Services;
using Template.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Template.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken = default)
    {
        var result = await _productService.GetProducts(cancellationToken);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _productService.GetProduct(id, cancellationToken);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDTORequest entityDTO, CancellationToken cancellationToken = default)
    {
        var result = await _productService.AddProduct(entityDTO, cancellationToken);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductDTORequest entityDTO, CancellationToken cancellationToken = default)
    {
        var result = await _productService.UpdateProduct(id, entityDTO, cancellationToken);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _productService.DeleteProduct(id, cancellationToken);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }
}