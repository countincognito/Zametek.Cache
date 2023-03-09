using System;

namespace Zametek.Utility.Cache
{
    [Serializable]
    public class RefreshCachedValueRequest
    {
        public string Key { get; set; }
    }
}
