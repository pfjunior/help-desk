using Microsoft.AspNetCore.Mvc;

namespace HD.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        return Ok("Hello World!");
    }
}
