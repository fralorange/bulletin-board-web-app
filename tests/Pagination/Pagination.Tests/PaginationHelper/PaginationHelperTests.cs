using BulletinBoard.Application.AppServices.Pagination.Helpers;

namespace Pagination.Tests.PaginationHelper
{
    public class PaginationHelperTests
    {
        [Fact]
        public async Task SplitByPages_ShouldReturnCorrectPage()
        {
            // Arrange
            var modelCollection = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var pageSize = 2;
            var pageIndex = 3;

            // Act
            var result = await PaginationHelper<int>.SplitByPages(Task.FromResult((IReadOnlyCollection<int>)modelCollection), pageSize, pageIndex);

            // Assert
            Assert.Equal(pageSize, result.Count);
            Assert.Equal(modelCollection.Skip(pageSize * pageIndex).Take(pageSize), result);
        }
    }
}