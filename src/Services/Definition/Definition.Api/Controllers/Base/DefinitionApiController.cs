using Career.Mvc.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers.Base;

[Authorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public abstract class DefinitionApiController: CareerApiController
{
        
}