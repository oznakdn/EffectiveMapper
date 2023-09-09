namespace Example.Api.Dto;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
