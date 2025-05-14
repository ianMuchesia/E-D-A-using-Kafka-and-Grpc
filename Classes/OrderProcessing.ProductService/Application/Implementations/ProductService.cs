


using OrderProcessing.ProductService.Application.Interfaces;
using OrderProcessing.ProductService.Contracts;
using OrderProcessing.ProductService.Domain.Entities;

namespace OrderProcessing.ProductService.Application.Implementations;


public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task AddProductAsync(CreateProductDto product, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding product: {Product}", product.Name);
        var newProduct = new Domain.Entities.Product(product.Name, product.Price, product.Stock);


        await _productRepository.AddProductAsync(newProduct, cancellationToken);


        _logger.LogInformation("Product added successfully: {Product}", product.Name);
    }

  
    public async Task AddStockAsync(AddStockDto addStockDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding stock for product with ID: {Id}", addStockDto.Id);
        var product = await _productRepository.GetProductByIdAsync(addStockDto.Id, cancellationToken);
        if (product == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found", addStockDto.Id);
            throw new KeyNotFoundException($"Product with ID: {addStockDto.Id} not found.");
        }

        product.UpdateStock(addStockDto.Stock);
        await _productRepository.UpdateProductAsync(product, cancellationToken);
        _logger.LogInformation("Stock added successfully for product with ID: {Id}", addStockDto.Id);
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting product with ID: {Id}", id);
        var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
        if (product == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found", id);
            throw new KeyNotFoundException($"Product with ID: {id} not found.");
        }

        await _productRepository.DeleteProductAsync(product, cancellationToken);
        _logger.LogInformation("Product with ID: {Id} deleted successfully", id);
    }

  
    public async Task<IEnumerable<Domain.Entities.Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving all products");
        var products = await _productRepository.GetAllProductsAsync(cancellationToken);
        _logger.LogInformation("Retrieved {Count} products", products.Count());
        return products;
    }

    public async Task<Domain.Entities.Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving product with ID: {Id}", id);
        var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
        if (product == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found", id);
            throw new KeyNotFoundException($"Product with ID: {id} not found.");
        }
        else
        {
            _logger.LogInformation("Retrieved product with ID: {Id}", id);
        }

        return product;
    }

 
 
    public async Task UpdateProductAsync(UpdateProductDto product, CancellationToken cancellationToken = default)
    {
        
        _logger.LogInformation("Updating product with ID: {Id}", product.Id);
        var existingProduct = await _productRepository.GetProductByIdAsync(product.Id, cancellationToken);
        if (existingProduct == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found", product.Id);
            throw new KeyNotFoundException($"Product with ID: {product.Id} not found.");
        }

        existingProduct.UpdateProduct(product.Name, product.Price, product.Stock);
       await _productRepository.UpdateProductAsync(existingProduct, cancellationToken);
    }
}