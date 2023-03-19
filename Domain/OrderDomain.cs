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
        if (product is null || customer is null)
            throw new ArgumentException($"{customer} and {product} can not be a null");

        ValidateDeliveryDateIfInvalidThrowException(deliveryDate);
        
        Product = product;
        Customer = customer;
        DeliveryDate = deliveryDate;
        Delivered = delivered;
    }

    public bool ChangeDeliveryDate(DateTime newDate)
    {
        ValidateDeliveryDateIfInvalidThrowException(newDate);
        
        if (Delivered == true)
            return false;
        
        DeliveryDate = newDate;
        return true;
    }
    public bool ChangeDeliveryStatus(bool newStatus)
    {
        Delivered = newStatus;
        return Delivered;
    }

    private void ValidateDeliveryDateIfInvalidThrowException(DateTime deliveryDate)
    {
        if (deliveryDate <= DateTime.Now && Delivered == false)
            throw new ArgumentException($"Invalid: {nameof(DeliveryDate)} argument. Delivery date can not be less than current date");
    }
}