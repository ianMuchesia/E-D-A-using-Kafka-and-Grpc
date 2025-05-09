





namespace OrderProcessing.ProductService.Domain.Entities;


public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private  set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }


    //for ef core
    private Product() { }

    public Product( string name, decimal price, int stock)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Stock = stock;
        
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0 && Math.Abs(quantity) > Stock)
            throw new InvalidOperationException("Insufficient stock to fulfill the order.");

        Stock += quantity;
    }
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        Price = newPrice;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Product name cannot be empty.");

        Name = newName;
    }


    public void UpdateProduct(string name, decimal price, int stock)
    {
        UpdateName(name);
        UpdatePrice(price);
        UpdateStock(stock);
    }



}