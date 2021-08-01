using System.Threading;
using System.Threading.Tasks;
using API.Controllers.Base;
using Application.Flows.Assets.Commands;
using Application.Models;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers.Assets
{
    public class CreateAssetController : ApiControllerBase
    {
        private readonly IRequestHandler<CreateAssetCommand, AssetObjectModel> _handler;

        public CreateAssetController(IRequestHandler<CreateAssetCommand, AssetObjectModel> handler, ILogger<ApiControllerBase> logger) : base(logger)
        {
            _handler = handler;
        }

        [HttpPost("assets")]
        public async Task<ActionResult<IResponseModel<AssetObjectModel>>> CreateAsset([FromBody] CreateAssetCommand command, CancellationToken cancellationToken)
        {
            return new ActionResult<IResponseModel<AssetObjectModel>>(new ResponseModel<AssetObjectModel>
            {
                Message = "Asset created successfully.",
                Data = await _handler.HandleAsync(command, cancellationToken),
                Total = 1
            });
        }
    }
}