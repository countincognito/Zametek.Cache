using System;

namespace Zametek.Utility.Cache
{
    [Serializable]
    public class GetCachedValueRequest
    {
        public string Key { get; set; }
    }
}
