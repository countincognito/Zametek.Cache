using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Zametek.Utility.Cache
{
    public class DeleteCachedValueRequestValidator
        : AbstractValidator<DeleteCachedValueRequest>
    {
        private static readonly DeleteCachedValueRequestValidator s_Instance = new DeleteCachedValueRequestValidator();

        protected DeleteCachedValueRequestValidator()
        {
            RuleFor(request => request).NotNull();
            RuleFor(request => request.Key).NotEmpty();
        }

        // https://stackoverflow.com/questions/42365741/fluent-validator-withmessage-and-singleton-instance
        public static async Task ValidateAndThrowAsync(
            DeleteCachedValueRequest request,
            CancellationToken ct)
        {
            await s_Instance
                .ValidateAndThrowAsync(request, cancellationToken: ct)
                .ConfigureAwait(false);
        }
    }
}
