using System;
using Zametek.Utility.Logging;

namespace Zametek.Utility.Cache
{
    [Serializable]
    public class GetCachedValueResponse
    {
        [NoLogging]
        public byte[] Data { get; set; }
    }
}
