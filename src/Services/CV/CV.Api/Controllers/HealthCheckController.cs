using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/HealthCheck")]
public class HealthCheckController : Controller
{
    [HttpGet("api-status")]
    [HttpHead("api-status")]
    public ActionResult ApiStatus()
    {
        return Ok("CV Api is awake!");
    }
}