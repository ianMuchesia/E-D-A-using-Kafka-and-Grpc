namespace OrderProcessing.ProductService.Contracts;



public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }


}