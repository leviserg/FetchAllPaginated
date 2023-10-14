namespace FetchAllPaginated.Tests
{
    [TestClass]
    public class CollectorTests
    {
        [TestMethod]
        public void FetchAllApiDataParallel_ShouldReturnExactItemCountWhenHasMore()
        {
            // Arrange
            var collector = new Collector();

            // Act
            int expectedTotalCount = ApiDataProvider.GetTotalCount();
            int requestedCount = expectedTotalCount - 2;
            Dictionary<int, string> result = collector.FetchAllApiDataParallel(requestedCount);

            // Assert
            Assert.AreEqual((requestedCount < 0) ? 0 : requestedCount, result.Count);
        }

        [TestMethod]
        public void FetchAllApiDataParallel_ShouldReturnExactItemCountWhenHasLess()
        {
            // Arrange
            var collector = new Collector();

            // Act
            int expectedTotalCount = ApiDataProvider.GetTotalCount();
            int requestedCount = expectedTotalCount + 2;
            Dictionary<int, string> result = collector.FetchAllApiDataParallel(requestedCount);

            // Assert
            Assert.AreEqual(expectedTotalCount, result.Count);
        }

        [TestMethod]
        public void FetchAllApiDataSequential_ShouldReturnExactItemCountWhenHasMore()
        {
            // Arrange
            var collector = new Collector();

            // Act
            int expectedTotalCount = ApiDataProvider.GetTotalCount();
            int requestedCount = expectedTotalCount - 2;
            Dictionary<int, string> result = collector.FetchAllApiDataSequential(requestedCount);

            // Assert
            Assert.AreEqual((requestedCount < 0) ? 0 : requestedCount, result.Count);
        }

        [TestMethod]
        public void FetchAllApiDataSequential_ShouldReturnExactItemCountWhenHasLess()
        {
            // Arrange
            var collector = new Collector();

            // Act
            int expectedTotalCount = ApiDataProvider.GetTotalCount();
            int requestedCount = expectedTotalCount + 2;
            Dictionary<int, string> result = collector.FetchAllApiDataSequential(requestedCount);

            // Assert
            Assert.AreEqual(expectedTotalCount, result.Count);
        }
    }
}