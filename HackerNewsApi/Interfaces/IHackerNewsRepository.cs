using HackerNewsApi.Model;

namespace HackerNewsApi.Interfaces
{
    public interface IHackerNewsRepository
    {
        Task<List<int>> GetNewestStoriesAsync();

        Task<HackerNews> GetNewsItemAsync(int id);
    }
}
