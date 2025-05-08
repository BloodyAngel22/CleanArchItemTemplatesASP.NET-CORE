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
    public async Task<IActionResult> GetProducts()
    {
        var result = await _productService.GetProducts();

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var result = await _productService.GetProduct(id);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDTORequest entityDTO)
    {
        var result = await _productService.AddProduct(entityDTO);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductDTORequest entityDTO)
    {
        var result = await _productService.UpdateProduct(id, entityDTO);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await _productService.DeleteProduct(id);

        return result.Success
            ? ResponseHelper.Ok(result.Data)
            : ResponseHelper.Error(result.Message);
    }
}