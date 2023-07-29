using System.Threading;
using System.Threading.Tasks;

namespace Zametek.Utility.Cache
{
    public interface ICacheUtility
    {
        Task<GetCachedValueResponse> GetCachedValueAsync(GetCachedValueRequest request, CancellationToken ct = default);

        Task RefreshCachedValueAsync(RefreshCachedValueRequest request, CancellationToken ct = default);

        Task DeleteCachedValueAsync(DeleteCachedValueRequest request, CancellationToken ct = default);

        Task SetCachedValueAsync(SetCachedValueRequest request, CancellationToken ct = default);
    }
}
