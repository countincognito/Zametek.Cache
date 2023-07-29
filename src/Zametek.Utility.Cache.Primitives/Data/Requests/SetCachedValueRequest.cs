using Destructurama.Attributed;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace Zametek.Utility.Cache
{
    [Serializable]
    public class SetCachedValueRequest
    {
        public string Key { get; set; }

        [NotLogged]
        public byte[] Data { get; set; }

        public DistributedCacheEntryOptions Options { get; set; }
    }
}
