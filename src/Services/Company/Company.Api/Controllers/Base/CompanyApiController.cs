using Career.Mvc.Base;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers.Base
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class CompanyApiController: CareerApiController
    {
        
    }
}