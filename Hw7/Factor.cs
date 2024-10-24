public class Factor
{
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public int TotalAmount { get; set; }
    public DateTime FactorDate { get; set; }
    public OrderStatus Status { get; set; }

    public Factor(List<Product> products, int totalPrice)
    {
        Products = products;
        TotalAmount = totalPrice;
        FactorDate = DateTime.Now;
        Status = OrderStatus.Registered;

    }
}