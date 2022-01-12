using Microsoft.AspNetCore.Mvc;

namespace Career.Identity.Quickstart.HealtCheck
{
    [Route("api/HealthCheck")]
    public class HealthCheckController : Controller
    {
        [HttpGet("api-status")]
        [HttpHead("api-status")]
        public ActionResult ApiStatus()
        {
            return Ok("Identity Api is awake!");
        }
    }
}