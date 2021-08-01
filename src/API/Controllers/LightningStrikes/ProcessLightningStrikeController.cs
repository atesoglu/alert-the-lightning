using System.Threading;
using System.Threading.Tasks;
using API.Controllers.Base;
using Application.Flows.LightningStrikes.Commands;
using Application.Models;
using Application.Request;
using Application.Response;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers.LightningStrikes
{
    public class ProcessLightningStrikeController : ApiControllerBase
    {
        private readonly IRequestHandler<ProcessLightningStrikeCommand, LightningStrikeObjectModel> _handler;

        public ProcessLightningStrikeController(IRequestHandler<ProcessLightningStrikeCommand, LightningStrikeObjectModel> handler, ILogger<ApiControllerBase> logger) : base(logger)
        {
            _handler = handler;
        }

        [HttpPost("lightning-strikes")]
        public async Task<ActionResult<IResponseModel>> ProcessLightningStrike([FromBody] ProcessLightningStrikeCommand command, CancellationToken cancellationToken)
        {
            IResponseModel response = command.FlashType == FlashTypes.Heartbeat
                ? new ResponseModel {Message = "I'm alive."}
                : new ResponseModel<LightningStrikeObjectModel> {Message = "Lightning strike info processed successfully.", Data = await _handler.HandleAsync(command, cancellationToken), Total = 1};

            return new ActionResult<IResponseModel>(response);
        }
    }
}