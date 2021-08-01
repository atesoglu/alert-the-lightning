using API.Controllers.Base;
using Application.Cache;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class ClearCacheController : ApiControllerBase
    {
        public ClearCacheController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        [HttpDelete("cache")]
        public ActionResult<IResponseModel> ClearCache()
        {
            AssetTokenSourceProvider.ResetCancellationToken();
            //LightningStrikeTokenSourceProvider.ResetCancellationToken();

            return new ActionResult<IResponseModel>(new ResponseModel {Message = "Cache cleared successfully."});
        }
    }
}