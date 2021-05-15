using Career.Mvc.Base;
using Microsoft.AspNetCore.Mvc;

namespace CV.Api.Controllers.Base
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public abstract class CVApiControllerBase: CareerApiController
    {
        
    }
}