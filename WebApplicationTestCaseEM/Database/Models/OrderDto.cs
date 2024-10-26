namespace WebApplicationTestCaseEM.Database.Models;

public class OrderDto
{
    public int Id { get; init; }
    public double Weight { get; init; }
    public required string DeliveryTime { get; init; }
    public int DistrictId { get; init; }
}