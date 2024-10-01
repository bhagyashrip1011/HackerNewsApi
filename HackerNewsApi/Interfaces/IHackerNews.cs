using HackerNewsApi.Model;

namespace HackerNewsApi.Interfaces
{
    public interface IHackerNewsService
    {
        Task<HackerNewsResponse> GetNewsAsync(int pageNo, int pageSize);
    }
}
