using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zametek.Utility.Cache
{
    public static class CacheUtilityExtensions
    {
        public async static Task<T> GetAsync<T>(
            this ICacheUtility cacheUtility,
            string key,
            CancellationToken ct) where T : class
        {
            if (cacheUtility is null)
            {
                throw new ArgumentNullException(nameof(cacheUtility));
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var request = new GetCachedValueRequest
            {
                Key = key,
            };

            GetCachedValueResponse response = await cacheUtility
                .GetCachedValueAsync(request, ct)
                .ConfigureAwait(false);

            byte[] data = response?.Data;

            if (data is null)
            {
                return null;
            }

            return data.ByteArrayToObject<T>();
        }

        public async static Task RefreshAsync(
            this ICacheUtility cacheUtility,
            string key,
            CancellationToken ct)
        {
            if (cacheUtility is null)
            {
                throw new ArgumentNullException(nameof(cacheUtility));
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var request = new RefreshCachedValueRequest
            {
                Key = key,
            };

            await cacheUtility
                .RefreshCachedValueAsync(request, ct)
                .ConfigureAwait(false);
        }

        public async static Task DeleteAsync(
            this ICacheUtility cacheUtility,
            string key,
            CancellationToken ct)
        {
            if (cacheUtility is null)
            {
                throw new ArgumentNullException(nameof(cacheUtility));
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var request = new DeleteCachedValueRequest
            {
                Key = key,
            };

            await cacheUtility
                .DeleteCachedValueAsync(request, ct)
                .ConfigureAwait(false);
        }

        public async static Task SetAsync<T>(
            this ICacheUtility cacheUtility,
            string key,
            T value,
            CancellationToken ct) where T : class
        {
            await cacheUtility
                .SetAsync(key, value, null, ct)
                .ConfigureAwait(false);
        }

        public async static Task SetAsync<T>(
            this ICacheUtility cacheUtility,
            string key,
            T value,
            DistributedCacheEntryOptions options,
            CancellationToken ct) where T : class
        {
            if (cacheUtility is null)
            {
                throw new ArgumentNullException(nameof(cacheUtility));
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var request = new SetCachedValueRequest
            {
                Key = key,
                Data = value.ObjectToByteArray(),
                Options = options,
            };

            await cacheUtility
                .SetCachedValueAsync(request, ct)
                .ConfigureAwait(false);
        }
    }
}
