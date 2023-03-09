using System.Threading;
using System.Threading.Tasks;

namespace Zametek.Utility.Cache
{
    public interface ICacheUtility
    {
        Task<GetCachedValueResponse> GetCachedValueAsync(GetCachedValueRequest request, CancellationToken ct);

        Task RefreshCachedValueAsync(RefreshCachedValueRequest request, CancellationToken ct);

        Task DeleteCachedValueAsync(DeleteCachedValueRequest request, CancellationToken ct);

        Task SetCachedValueAsync(SetCachedValueRequest request, CancellationToken ct);
    }
}
