namespace OrdersApi.Models;

public class OrderModel
{
    public Guid Id { get; set; }
    public ProductModel Product { get; set; }
    public CustomerForOrderModel Customer { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool Delivered { get; set; }
}