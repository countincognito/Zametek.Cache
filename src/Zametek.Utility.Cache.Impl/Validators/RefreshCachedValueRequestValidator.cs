using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Zametek.Utility.Cache
{
    public class RefreshCachedValueRequestValidator
        : AbstractValidator<RefreshCachedValueRequest>
    {
        private static readonly RefreshCachedValueRequestValidator s_Instance = new RefreshCachedValueRequestValidator();

        protected RefreshCachedValueRequestValidator()
        {
            RuleFor(request => request).NotNull();
            RuleFor(request => request.Key).NotEmpty();
        }

        // https://stackoverflow.com/questions/42365741/fluent-validator-withmessage-and-singleton-instance
        public static async Task ValidateAndThrowAsync(
            RefreshCachedValueRequest request,
            CancellationToken ct)
        {
            await s_Instance
                .ValidateAndThrowAsync(request, cancellationToken: ct)
                .ConfigureAwait(false);
        }
    }
}
