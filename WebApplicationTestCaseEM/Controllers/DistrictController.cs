using Microsoft.AspNetCore.Mvc;
using WebApplicationTestCaseEM.Database;
using WebApplicationTestCaseEM.Models;

namespace WebApplicationTestCaseEM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DistrictController(DatabaseContext context, ILogger<DistrictController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult GetDistricts()
    {
        var districts = context.GetDistricts();
        logger.LogInformation("Get districts: count={count}", districts.Length);
        return Ok(districts);
    }

    [HttpPost]
    public IActionResult AddDistrict([FromBody] DistrictDto districtDto)
    {
        var id = context.AddDistrict(districtDto.Name);
        logger.LogInformation("New district added: id={id}, name={name}", id, districtDto.Name);
        return Ok(id);
    }
}