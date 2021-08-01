using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Cache;
using Application.Models;
using Application.Persistence;
using Application.Request;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Flows.Assets.Queries
{
    public class ListAssetsHandler : IRequestHandler<ListAssetsQuery, ICollection<AssetObjectModel>>
    {
        private const string CacheKey = "cached-assets";

        private readonly IDataContext _dbContext;
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ListAssetsHandler> _logger;

        public ListAssetsHandler(IDataContext dbContext, IEventDispatcherService eventDispatcherService, IMemoryCache cache, ILogger<ListAssetsHandler> logger)
        {
            _dbContext = dbContext;
            _eventDispatcherService = eventDispatcherService;
            _cache = cache;
            _logger = logger;
        }

        public async Task<ICollection<AssetObjectModel>> HandleAsync(ListAssetsQuery request, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(CacheKey, out ICollection<AssetObjectModel> cached)) return cached;

            var domainList = await _dbContext.Assets.ToListAsync(cancellationToken);
            cached = new List<AssetObjectModel>();

            domainList?.ToList().ForEach(domain => cached.Add(new AssetObjectModel(domain)));

            if (cached.Count == 0) return null;

            _cache.Set(CacheKey, cached, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)).AddExpirationToken(AssetTokenSourceProvider.GetCancellationToken()));

            return cached;
        }
    }
}