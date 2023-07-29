using Destructurama.Attributed;
using System;

namespace Zametek.Utility.Cache
{
    [Serializable]
    public class GetCachedValueResponse
    {
        [NotLogged]
        public byte[] Data { get; set; }
    }
}
