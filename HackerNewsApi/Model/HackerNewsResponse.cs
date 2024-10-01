namespace HackerNewsApi.Model
{
    public class HackerNewsResponse
    {
        public int totalNews { get; set; }
        public IEnumerable<HackerNews> news { get; set; }
    }
}
