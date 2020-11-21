using System;
using Company.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Company.Api.Controllers
{
    public class CompanyController: CompanyApiController
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
            _logger.LogInformation("company api created!");
        }

        public IActionResult Test()
        {
            throw new NotImplementedException("henuz hazır değil!");
        }
    }
}