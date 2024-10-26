using Microsoft.AspNetCore.Mvc;
using WebApplicationTestCaseEM.Database;

namespace WebApplicationTestCaseEM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(DatabaseContext context, ILogger<OrderController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders([FromQuery] int? districtId, [FromQuery] DateTime? deliveryStartFrom = null)
    {
        var deliveryStartTo = deliveryStartFrom?.AddMinutes(30);
        var filterResult = context.GetOrders(districtId, deliveryStartFrom, deliveryStartTo);
        logger.LogInformation("Get orders by: districtId={districtId}, deliveryStartFrom={deliveryStartTo}, count={count}", districtId, deliveryStartTo, filterResult.Length);
        return Ok(filterResult);
    }
}