using HackerNewsApi.Interfaces;

namespace TestHackerApi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task BestStoriesEndpoint_Return_Status200OK()
        {
            // Arrange
            Mock<IHackerNewsRepository> mockBestStoriesService = new();
            mockBestStoriesService.Setup(
                s => s.GetBestStoriesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<IEnumerable<Story>?>(DataUtility.GetBestStories().OrderByDescending(s => s.score).Take(5)));

            // Act
            IResult resultObject = await BestStoriesEndpoint.GetBestStories(5, mockBestStoriesService.Object, CancellationToken.None)
                .ConfigureAwait(false);

            //Assert
            mockBestStoriesService.Verify(s => s.GetBestStoriesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

            var result = resultObject as Ok<IEnumerable<Story>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(5, result.Value.Count());
        }
    }
}