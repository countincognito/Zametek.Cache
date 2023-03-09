using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zametek.Utility.Logging;

namespace Zametek.Utility.Cache
{
    [DiagnosticLogging(LogActive.On)]
    public class CacheUtility
        : ICacheUtility
    {
        #region Fields

        private readonly IDistributedCache m_DistributedCache;

        private readonly DistributedCacheEntryOptions m_DefaultDistributedCacheEntryOptions = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromHours(1)
        };

        #endregion

        #region Ctors

        public CacheUtility(IDistributedCache distributedCache)
        {
            m_DistributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        #endregion

        #region ICacheUtility Members

        public async Task<GetCachedValueResponse> GetCachedValueAsync(
            GetCachedValueRequest request,
            CancellationToken ct)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            await GetCachedValueRequestValidator
                .ValidateAndThrowAsync(request, ct)
                .ConfigureAwait(false);

            byte[] data = await m_DistributedCache
                .GetAsync(request.Key, ct)
                .ConfigureAwait(false);

            if (data is null)
            {
                return null;
            }

            return new GetCachedValueResponse
            {
                Data = data,
            };
        }

        public async Task RefreshCachedValueAsync(
            RefreshCachedValueRequest request,
            CancellationToken ct)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            await RefreshCachedValueRequestValidator
                .ValidateAndThrowAsync(request, ct)
                .ConfigureAwait(false);

            await m_DistributedCache
                .RefreshAsync(request.Key, ct)
                .ConfigureAwait(false);
        }

        public async Task DeleteCachedValueAsync(
            DeleteCachedValueRequest request,
            CancellationToken ct)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            await DeleteCachedValueRequestValidator
                .ValidateAndThrowAsync(request, ct)
                .ConfigureAwait(false);

            await m_DistributedCache
                .RemoveAsync(request.Key, ct)
                .ConfigureAwait(false);
        }

        public async Task SetCachedValueAsync(
            SetCachedValueRequest request,
            CancellationToken ct)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            await SetCachedValueRequestValidator
                .ValidateAndThrowAsync(request, ct)
                .ConfigureAwait(false);

            await m_DistributedCache
                .SetAsync(request.Key, request.Data, request.Options ?? m_DefaultDistributedCacheEntryOptions, ct)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
