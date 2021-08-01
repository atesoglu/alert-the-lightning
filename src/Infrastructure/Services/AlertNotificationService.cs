using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Flows.Assets.Queries;
using Application.Models;
using Application.Request;
using Application.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class AlertNotificationService : IAlertNotificationService
    {
        private const string CachePrefix = "alert-for-quad:";

        private readonly IRequestHandler<ListAssetsQuery, ICollection<AssetObjectModel>> _listAssetHandler;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AlertNotificationService> _logger;

        public AlertNotificationService(IRequestHandler<ListAssetsQuery, ICollection<AssetObjectModel>> listAssetHandler, IMemoryCache cache, ILogger<AlertNotificationService> logger)
        {
            _listAssetHandler = listAssetHandler;
            _cache = cache;
            _logger = logger;
        }

        public async Task ProcessAsync(LightningStrikeObjectModel objectModel, CancellationToken cancellationToken)
        {
            var quadKey = objectModel.LocationToQuadKey();

            if (_cache.TryGetValue(CachePrefix + quadKey, out string _)) return;

            var assets = await _listAssetHandler.HandleAsync(new ListAssetsQuery(), cancellationToken);

            var asset = assets?.FirstOrDefault(w => w.QuadKey == quadKey);
            if (asset != null)
            {
                _logger.LogInformation("Lightning alert for {AssetOwner}:{AssetName}", asset.AssetOwner, asset.AssetName);
                _cache.Set(CachePrefix + quadKey, quadKey, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

                return;
            }

            _logger.LogDebug("None of the assets has been struck by the LightningStrikeId: {LightningStrikeId}.", objectModel.LightningStrikeId);
        }
    }
}