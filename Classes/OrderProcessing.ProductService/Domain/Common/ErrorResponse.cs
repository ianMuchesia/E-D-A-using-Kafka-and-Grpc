namespace OrderProcessing.ProductService.Domain.Common;



public class ErrorResponse
{
    public string Title { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}