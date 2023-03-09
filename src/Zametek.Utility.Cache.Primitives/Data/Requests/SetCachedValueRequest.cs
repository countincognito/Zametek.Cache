using Microsoft.Extensions.Caching.Distributed;
using System;
using Zametek.Utility.Logging;

namespace Zametek.Utility.Cache
{
    [Serializable]
    public class SetCachedValueRequest
    {
        public string Key { get; set; }

        [NoLogging]
        public byte[] Data { get; set; }

        public DistributedCacheEntryOptions Options { get; set; }
    }
}
