using Microsoft.AspNetCore.Mvc;
using OrderProcessing.ProductService.Application.Interfaces;
using OrderProcessing.ProductService.Contracts;
namespace OrderProcessing.ProductService.API.Controllers;



[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productRepository,  ILogger<ProductController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting product with ID: {Id}", id);
        var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
        if (product == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found", id);
            return NotFound($"Product with ID: {id} not found.");
        }
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all products");
        var products = await _productRepository.GetAllProductsAsync(cancellationToken);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDto product, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding product: {Product}", product.Name);
        await _productRepository.AddProductAsync(product, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { Name = product.Name }, product);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto product, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating product with ID: {Id}", id);
        var existingProduct = await _productRepository.GetProductByIdAsync(id, cancellationToken);
        if (existingProduct == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found", id);
            return NotFound($"Product with ID: {id} not found.");
        }
        await _productRepository.UpdateProductAsync(product, cancellationToken);
        return NoContent();
    }


    [HttpPost("add-stock")]
    public async Task<IActionResult> AddStock([FromBody] AddStockDto addStockDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding stock for product with ID: {Id}", addStockDto.Id);
        await _productRepository.AddStockAsync(addStockDto, cancellationToken);
        return NoContent();
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting product with ID: {Id}", id);
        var existingProduct = await _productRepository.GetProductByIdAsync(id, cancellationToken);
        if (existingProduct == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found", id);
            return NotFound($"Product with ID: {id} not found.");
        }
        await _productRepository.DeleteProductAsync(id, cancellationToken);
        return NoContent();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAllProducts(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting all products");
        var products = await _productRepository.GetAllProductsAsync(cancellationToken);
        foreach (var product in products)
        {
            await _productRepository.DeleteProductAsync(product.Id, cancellationToken);
        }
        return NoContent();
    }

}

