using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Zametek.Utility.Cache
{
    public class SetCachedValueRequestValidator
        : AbstractValidator<SetCachedValueRequest>
    {
        private static readonly SetCachedValueRequestValidator s_Instance = new SetCachedValueRequestValidator();

        protected SetCachedValueRequestValidator()
        {
            RuleFor(request => request).NotNull();
            RuleFor(request => request.Key).NotEmpty();
            RuleFor(request => request.Data).NotNull();
        }

        // https://stackoverflow.com/questions/42365741/fluent-validator-withmessage-and-singleton-instance
        public static async Task ValidateAndThrowAsync(
            SetCachedValueRequest request,
            CancellationToken ct)
        {
            await s_Instance
                .ValidateAndThrowAsync(request, cancellationToken: ct)
                .ConfigureAwait(false);
        }
    }
}
