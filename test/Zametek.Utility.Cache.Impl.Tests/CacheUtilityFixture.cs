using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Zametek.Utility.Logging;

namespace Zametek.Utility.Cache.Impl.Tests
{
    public class CacheUtilityFixture
        : IDisposable
    {
        public CacheUtilityFixture()
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
