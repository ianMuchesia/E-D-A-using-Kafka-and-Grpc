namespace OrderProcessing.ProductService.Contracts;


public class UpdateProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    
}