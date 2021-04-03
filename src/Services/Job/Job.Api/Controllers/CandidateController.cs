using System;
using System.Threading.Tasks;
using Job.Api.Controllers.Base;
using Job.Application.Candidate.Commands.Withdraw;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Job.Api.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/candidates")]
    public class CandidateController: JobApiController
    {
        private readonly IMediator _mediator;

        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Withdraw application 
        /// </summary>
        /// <param name="id">Candidate id</param>
        [HttpPut("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid id)
            => Ok(await _mediator.Send(new WithdrawCandidateCommand(id)));
    }
}