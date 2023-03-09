using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Zametek.Utility.Cache
{
    public class GetCachedValueRequestValidator
        : AbstractValidator<GetCachedValueRequest>
    {
        private static readonly GetCachedValueRequestValidator s_Instance = new GetCachedValueRequestValidator();

        protected GetCachedValueRequestValidator()
        {
            RuleFor(request => request).NotNull();
            RuleFor(request => request.Key).NotEmpty();
        }

        // https://stackoverflow.com/questions/42365741/fluent-validator-withmessage-and-singleton-instance
        public static async Task ValidateAndThrowAsync(
            GetCachedValueRequest request,
            CancellationToken ct)
        {
            await s_Instance
                .ValidateAndThrowAsync(request, cancellationToken: ct)
                .ConfigureAwait(false);
        }
    }
}
