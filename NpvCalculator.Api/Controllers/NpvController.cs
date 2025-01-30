using MediatR;
using Microsoft.AspNetCore.Mvc;
using NpvCalculator.Application.Npv.Commands;
using NpvCalculator.Application.Npv.Queries;
using NpvCalculator.Core.Entities;

namespace NpvCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NpvController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NpvController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<decimal>> Calculate([FromBody] CalculateNpvCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<NpvCalculation>>> GetHistory()
        {
            var query = new GetCalculationHistoryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
