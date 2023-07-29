using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Zametek.Utility.Logging;

namespace Zametek.Utility.Cache.Extra.Tests
{
    public class CacheUtilityExtensionsFixture
        : IDisposable
    {
        public CacheUtilityExtensionsFixture()
        {
            ILogger serilog = new LoggerConfiguration().CreateLogger();
            ServerServices = new ServiceCollection()
                .ActivateLogTypes(LogTypes.Tracking | LogTypes.Diagnostic | LogTypes.Error)
                .TryAddSingletonWithLogProxy<ICacheUtility, CacheUtility>()
                .AddSingleton(serilog)
                .AddDistributedMemoryCache()
                .BuildServiceProvider();
        }

        public IServiceProvider ServerServices { get; private set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
