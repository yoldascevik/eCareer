using Microsoft.AspNetCore.Mvc;

namespace Career.Mvc.Base
{
    [ApiController]
    public class CareerApiController : ControllerBase
    {
        [Route("api/ping")]
        public IActionResult Ping() => Ok("Pong!");
    }
}