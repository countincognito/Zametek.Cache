using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Zametek.Utility.Cache.Impl.Tests
{
    public class CacheUtilityTests
    {
        private readonly CacheUtilityFixture m_CacheUtilityFixture;

        public CacheUtilityTests()
        {
            m_CacheUtilityFixture = new CacheUtilityFixture();
        }

        public static string UniqueString() => Guid.NewGuid().ToFlatString();

        [Fact]
        public async Task CacheUtility_GivenGetCachedValueAsync_WhenCacheValueIsEmpty_ThenNullDataReturned()
        {
            ICacheUtility cacheUtility = m_CacheUtilityFixture.ServerServices.GetService<ICacheUtility>();

            var request = new GetCachedValueRequest
            {
                Key = UniqueString(),
            };

            var response = await cacheUtility.GetCachedValueAsync(request, default);

            response.Should().BeNull();
        }

        [Fact]
        public async Task CacheUtility_GivenGetCachedValueAsync_WhenCacheValueIsSet_ThenDataReturned()
        {
            ICacheUtility cacheUtility = m_CacheUtilityFixture.ServerServices.GetService<ICacheUtility>();

            var setRequest = new SetCachedValueRequest
            {
                Key = UniqueString(),
                Data = UniqueString().ObjectToByteArray(),
            };

            await cacheUtility.SetCachedValueAsync(setRequest, default);

            var getRequest = new GetCachedValueRequest
            {
                Key = setRequest.Key,
            };

            var response = await cacheUtility.GetCachedValueAsync(getRequest, default);

            response.Should().NotBeNull();
            response.Data.Should().BeEquivalentTo(setRequest.Data);
        }

        [Fact]
        public async Task CacheUtility_GivenGetCachedValueAsync_WhenCacheValueIsChanged_ThenNewDataReturned()
        {
            ICacheUtility cacheUtility = m_CacheUtilityFixture.ServerServices.GetService<ICacheUtility>();

            var setRequest = new SetCachedValueRequest
            {
                Key = UniqueString(),
                Data = UniqueString().ObjectToByteArray(),
            };

            await cacheUtility.SetCachedValueAsync(setRequest, default);

            var newSetRequest = new SetCachedValueRequest
            {
                Key = setRequest.Key,
                Data = UniqueString().ObjectToByteArray(),
            };

            await cacheUtility.SetCachedValueAsync(newSetRequest, default);

            var getRequest = new GetCachedValueRequest
            {
                Key = setRequest.Key,
            };

            var response = await cacheUtility.GetCachedValueAsync(getRequest, default);

            response.Should().NotBeNull();
            response.Data.Should().BeEquivalentTo(newSetRequest.Data);
        }

        [Fact]
        public async Task CacheUtility_GivenDeleteCachedValueAsync_WhenCacheValueIsEmpty_ThenNoExceptionThrown()
        {
            ICacheUtility cacheUtility = m_CacheUtilityFixture.ServerServices.GetService<ICacheUtility>();

            var deleteRequest = new DeleteCachedValueRequest
            {
                Key = UniqueString(),
            };

            await cacheUtility.DeleteCachedValueAsync(deleteRequest, default);
        }

        [Fact]
        public async Task CacheUtility_GivenDeleteCachedValueAsync_WhenCacheValueIsSet_ThenNullDataReturned()
        {
            ICacheUtility cacheUtility = m_CacheUtilityFixture.ServerServices.GetService<ICacheUtility>();

            var setRequest = new SetCachedValueRequest
            {
                Key = UniqueString(),
                Data = UniqueString().ObjectToByteArray(),
            };

            await cacheUtility.SetCachedValueAsync(setRequest, default);

            var deleteRequest = new DeleteCachedValueRequest
            {
                Key = setRequest.Key,
            };

            await cacheUtility.DeleteCachedValueAsync(deleteRequest, default);

            var getRequest = new GetCachedValueRequest
            {
                Key = setRequest.Key,
            };

            var response = await cacheUtility.GetCachedValueAsync(getRequest, default);

            response.Should().BeNull();
        }
    }
}
