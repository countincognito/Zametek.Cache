using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Zametek.Utility.Cache.Extra.Tests
{
    public class CacheUtilityExtensionsTests
    {
        private readonly CacheUtilityExtensionsFixture m_CacheUtilityExtensionsFixture;

        public CacheUtilityExtensionsTests()
        {
            m_CacheUtilityExtensionsFixture = new CacheUtilityExtensionsFixture();
        }

        public static string UniqueString() => Guid.NewGuid().ToFlatString();

        [Fact]
        public async Task CacheUtilityExtensions_GivenGetAsync_WhenCacheValueIsEmpty_ThenNullDataReturned()
        {
            ICacheUtility cacheUtility = m_CacheUtilityExtensionsFixture.ServerServices.GetService<ICacheUtility>();

            var response = await cacheUtility.GetAsync<string>(UniqueString());

            response.Should().BeNull();
        }

        [Fact]
        public async Task CacheUtilityExtensions_GivenGetAsync_WhenCacheValueIsSet_ThenDataReturned()
        {
            ICacheUtility cacheUtility = m_CacheUtilityExtensionsFixture.ServerServices.GetService<ICacheUtility>();

            var key = UniqueString();
            var data = UniqueString();

            await cacheUtility.SetAsync(key, data);
            string response = await cacheUtility.GetAsync<string>(key);

            response.Should().NotBeNull();
            response.Should().Be(data);
        }

        [Fact]
        public async Task CacheUtilityExtensions_GivenGetAsync_WhenCacheValueIsChanged_ThenNewDataReturned()
        {
            ICacheUtility cacheUtility = m_CacheUtilityExtensionsFixture.ServerServices.GetService<ICacheUtility>();

            var key = UniqueString();
            var data = UniqueString();

            await cacheUtility.SetAsync(key, data);

            var newData = UniqueString();

            await cacheUtility.SetAsync(key, newData);
            string response = await cacheUtility.GetAsync<string>(key);

            response.Should().NotBeNull();
            response.Should().Be(newData);
        }

        [Fact]
        public async Task CacheUtilityExtensions_GivenDeleteAsync_WhenCacheValueIsEmpty_ThenNoExceptionThrown()
        {
            ICacheUtility cacheUtility = m_CacheUtilityExtensionsFixture.ServerServices.GetService<ICacheUtility>();

            var key = UniqueString();

            await cacheUtility.DeleteAsync(key);
        }

        [Fact]
        public async Task CacheUtilityExtensions_GivenDeleteAsync_WhenCacheValueIsSet_ThenNullDataReturned()
        {
            ICacheUtility cacheUtility = m_CacheUtilityExtensionsFixture.ServerServices.GetService<ICacheUtility>();

            var key = UniqueString();
            var data = UniqueString();

            await cacheUtility.SetAsync(key, data);
            await cacheUtility.DeleteAsync(key);
            string response = await cacheUtility.GetAsync<string>(key);

            response.Should().BeNull();
        }
    }
}
