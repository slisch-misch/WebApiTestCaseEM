namespace WebApplicationTestCaseEM.Database.Entity;

public class Order
{
    public Order(int id, double weight, DateTime deliveryTime, int districtId)
    {
        if (id <= 0)
            throw new ArgumentException("Order id must be positive number");

        if (weight <= 0)
            throw new ArgumentException("Weight must be positive number");

        if (deliveryTime == default)
            throw new ArgumentException("Delivery time must be set");

        if (districtId <= 0)
            throw new ArgumentException("District id must be positive number");

        Id = id;
        Weight = weight;
        DeliveryTime = deliveryTime;
        DistrictId = districtId;
    }

    public int Id { get; set; }
    public double Weight { get; set; }
    public DateTime DeliveryTime { get; set; }
    public int DistrictId { get; set; }
}