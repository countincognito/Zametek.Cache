using System;

namespace Zametek.Utility.Cache
{
    [Serializable]
    public class DeleteCachedValueRequest
    {
        public string Key { get; set; }
    }
}
