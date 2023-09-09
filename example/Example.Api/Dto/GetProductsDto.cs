namespace Example.Api.Dto
{
    public class GetProductsDto
    {
        //public int Id { get; set; }
        //public string ProductName { get; set; }
        //public decimal Price { get; set; }
        //public int Quantity { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
