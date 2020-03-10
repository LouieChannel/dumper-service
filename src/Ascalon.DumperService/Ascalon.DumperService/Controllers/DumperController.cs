using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ascalon.DumperService.Features.Dumpers.PostDumper;
using System.Threading.Tasks;

namespace Ascalon.DumperService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DumperController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DumperController> _logger;

        public DumperController(ILogger<DumperController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody]List<PostDumperCommand> postDumperCommands)
        {
            try
            {
                foreach (PostDumperCommand postDumperCommand in postDumperCommands)
                   await _mediator.Send(postDumperCommand);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when trying to get data from dumper.");
                return StatusCode(500);
            }
        }
    }
}