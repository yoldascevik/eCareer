using Career.Mvc.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.Api.Controllers.Base;

[Authorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public abstract class JobApiController: CareerApiController
{
        
}