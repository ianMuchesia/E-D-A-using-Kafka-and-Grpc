namespace OrderProcessing.ProductService.Application.Interfaces;

using OrderProcessing.ProductService.Contracts;
using OrderProcessing.ProductService.Domain.Entities;



public interface IProductService
{
    Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task AddProductAsync(CreateProductDto product, CancellationToken cancellationToken = default);
    Task UpdateProductAsync(UpdateProductDto product, CancellationToken cancellationToken = default);

    Task AddStockAsync(AddStockDto addStockDto, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);
}