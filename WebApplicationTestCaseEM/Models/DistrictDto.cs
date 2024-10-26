using System.ComponentModel.DataAnnotations;

namespace WebApplicationTestCaseEM.Models;

public class DistrictDto
{
    [Required] public required string Name { get; set; }
}