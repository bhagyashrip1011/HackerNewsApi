using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Xunit;
using HackerNewsApi.Repository;

namespace HackerNews.Test
{
    public class HackerNewsRepositoryTest
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IMemoryCache> _cacheMock;
        private readonly HackerNewsRepository _service;

        public HackerNewsRepositoryTest()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _cacheMock = new Mock<IMemoryCache>();
            _service = new HackerNewsRepository(_httpClientFactoryMock.Object, _cacheMock.Object);
        }

        [Fact]
        public async Task GetNewestStoriesAsync_ReturnsCachedStories_WhenCacheIsSet()
        {
            // Arrange
            var cacheKey = "NewestStories";
            var cachedStories = new List<int> { 1, 2, 3 };

            // Setup cache to return the cached stories
            _cacheMock.Setup(x => x.TryGetValue(cacheKey, out cachedStories)).Returns(true);

            // Act
            var result = await _service.GetNewestStoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(cachedStories, result);

            // Verify that the cache was accessed and that HTTP client was not called
            _httpClientFactoryMock.Verify(x => x.CreateClient(It.IsAny<string>()), Times.Never);
        }

       
    }
}