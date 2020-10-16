using Career.Mvc.Base;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers.Base
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class DefinitionApiController: CareerApiController
    {
        
    }
}