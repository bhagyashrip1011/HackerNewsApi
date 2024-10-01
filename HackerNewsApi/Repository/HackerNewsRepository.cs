using HackerNewsApi.Interfaces;
using HackerNewsApi.Model;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsApi.Repository
{
    public class HackerNewsRepository : IHackerNewsRepository
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private string cacheKey;


        public HackerNewsRepository(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }
        public async Task<List<int>> GetNewestStoriesAsync()
        {
            cacheKey = "NewestStories";
            if (_cache.TryGetValue(cacheKey, out List<int> stories))
            {
                return stories;
            }

            var client = _httpClientFactory.CreateClient("ExternalApi");

            stories = await client.GetFromJsonAsync<List<int>>("newstories.json");


            _cache.Set(cacheKey, stories, TimeSpan.FromMinutes(1));
            return stories;
        }

        public async Task<HackerNews> GetNewsItemAsync(int id)
        {
            cacheKey = $"NewestStories" + id;
            if (_cache.TryGetValue(cacheKey, out HackerNews item))
            {
                return item;
            }

            var client = _httpClientFactory.CreateClient("ExternalApi");

            item = await client.GetFromJsonAsync<HackerNews>($"/v0/item/{id}.json");

            if(item != null)
            _cache.Set(cacheKey, item, TimeSpan.FromMinutes(1));

            return item;
        }
    }
}
