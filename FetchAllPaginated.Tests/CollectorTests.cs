namespace FetchAllPaginated.Tests
{
    [TestClass]
    public class CollectorTests
    {
        [TestMethod]
        public void FetchAllApiDataParallel_ShouldReturnExactItemCount()
        {
            // Arrange
            var collector = new Collector();

            // Act
            Dictionary<int, string> result = collector.FetchAllApiDataParallel();

            // Assert
            int expectedTotalCount = ApiDataProvider.GetTotalCount();
            Assert.AreEqual(expectedTotalCount, result.Count);
        }

        [TestMethod]
        public void FetchAllApiDataSequential_ShouldReturnExactItemCount()
        {
            // Arrange
            var collector = new Collector();

            // Act
            Dictionary<int, string> result = collector.FetchAllApiDataSequential();

            // Assert
            int expectedTotalCount = ApiDataProvider.GetTotalCount();
            Assert.AreEqual(expectedTotalCount, result.Count);
        }
    }
}