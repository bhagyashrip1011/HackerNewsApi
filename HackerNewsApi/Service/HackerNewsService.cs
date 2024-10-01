using HackerNewsApi.Interfaces;
using HackerNewsApi.Model;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsApi.Service
{
    public class HackerNewsService : IHackerNewsService
    {
        private IHackerNewsRepository _hackerNewsRepository;
        public HackerNewsService(IHackerNewsRepository hackerNewsRepository)
        {
            _hackerNewsRepository = hackerNewsRepository;
        }

        public async Task<HackerNewsResponse> GetNewsAsync(int pageNo, int pageSize)
        {
            HackerNewsResponse newsResponse = new HackerNewsResponse();
            var stories = await _hackerNewsRepository.GetNewestStoriesAsync();
            newsResponse.totalNews = stories.Count;
            var storyTasks = stories.Skip((pageNo - 1) * pageSize).Take(pageSize);
            List < HackerNews > storyResponse= new List<HackerNews>();
            foreach(int id in storyTasks)
            {
                HackerNews story = await _hackerNewsRepository.GetNewsItemAsync(id);
                if(story!=null)
                storyResponse.Add(story);
                //return storyResponse;
            };

            newsResponse.news = storyResponse;
            return newsResponse;

        }

    }
}
