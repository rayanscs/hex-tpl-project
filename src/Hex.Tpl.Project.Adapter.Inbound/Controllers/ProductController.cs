namespace Hex.Tpl.Project.Adapter.Inbound.Controllers;

using Hex.Tpl.Project.Application.DTOs;
using Hex.Tpl.Project.Domain.Entities;
using Hex.Tpl.Project.Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequest request)
    {
        try
        {
            var product = await _productService.CreateAsync(request.Name, request.Price);
            var response = new ProductResponse(product.Id, product.Name, product.Price, product.CreatedAt);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null) return NotFound();

        return Ok(new ProductResponse(product.Id, product.Name, product.Price, product.CreatedAt));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        var response = products.Select(p =>
            new ProductResponse(p.Id, p.Name, p.Price, p.CreatedAt));
        return Ok(response);
    }

    [HttpPatch("{id:guid}/price")]
    public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] UpdatePriceRequest request)
    {
        try
        {
            await _productService.UpdatePriceAsync(id, request.NewPrice);
            return NoContent();
        }
        catch (DomainException ex) when (ex.Message == "Produto não encontrado.")
        {
            return NotFound(ex.Message);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}
