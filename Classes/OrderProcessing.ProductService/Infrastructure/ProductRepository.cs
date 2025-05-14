



using Microsoft.EntityFrameworkCore;
using OrderProcessing.ProductService.AppDbContext;
using OrderProcessing.ProductService.Application.Interfaces;
using OrderProcessing.ProductService.Domain.Entities;

namespace OrderProcessing.ProductService.Infrastructure;



public class ProductRepository : IProductRepository
{


    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddProductAsync(Domain.Entities.Product product, CancellationToken cancellationToken = default)
    {
        await _dbContext.Product.AddAsync(product, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductAsync(Domain.Entities.Product product, CancellationToken cancellationToken = default)
    {
       
            _dbContext.Product.Remove(product);
            await SaveChangesAsync(cancellationToken);
        
    }

    public async Task<IEnumerable<Domain.Entities.Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Product.ToListAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Product.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductAsync(Domain.Entities.Product product, CancellationToken cancellationToken = default)
    {
        _dbContext.Product.Update(product);
        await SaveChangesAsync(cancellationToken);
    }
}