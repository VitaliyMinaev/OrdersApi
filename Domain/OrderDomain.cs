using Domain.Abstract;

namespace Domain;

public class OrderDomain : BaseDomain
{
    public ProductDomain Product { get; }
    public CustomerDomain Customer { get; }
    public DateTime DeliveryDate { get; private set; }
    public bool Delivered { get; private set; }
    
    public OrderDomain(Guid id, CustomerDomain customer, ProductDomain product, DateTime deliveryDate, bool delivered) : base(id)
    {
        Product = product;
        Customer = customer;
        DeliveryDate = deliveryDate;
        Delivered = delivered;
    }

    public bool ChangeDeliveryDate(DateTime newDate)
    {
        if (newDate < DateTime.Now && Delivered == false)
            throw new ArgumentException($"Invalid: {nameof(newDate)} argument. Delivery date can not be less than current date");

        if (Delivered == true)
            return false;
        
        DeliveryDate = newDate;
        return true;
    }
    public bool MarkAsDelivered()
    {
        Delivered = true;
        return Delivered;
    }
}