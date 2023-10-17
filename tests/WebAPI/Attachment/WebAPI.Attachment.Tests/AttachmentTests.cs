using BulletinBoard.Contracts.Attachment;
using System.Net;
using System.Net.Http.Json;
using WebAPI.Infrastructure.Tests;
using WebAPI.Infrastructure.Tests.Factories;

namespace WebAPI.Attachment.Tests
{
    public class AttachmentTests : IClassFixture<BulletinBoardWebApplicationFactory>
    {
        private readonly BulletinBoardWebApplicationFactory _webApplicationFactory;

        public AttachmentTests(BulletinBoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async void Test_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestAttachmentId;

            // Act
            var response = await httpClient.GetAsync($"attachment/get-by-id?id={id}");

            // Assert

            Assert.NotNull(response);

            var result = await response.Content.ReadFromJsonAsync<AttachmentDto>();

            Assert.NotNull(result);

            Assert.Equal(DataSeedHelper.TestAdvertId.ToString(), result!.Ad.Id.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_GetByIdAsync_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/attachment/get-by-id?id=00000000-0000-0000-0000-000000000000");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test_Ad_Delete_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"/attachment/{DataSeedHelper.TestAttachmentId}");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Проверить, что сущность была удалена
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var advert = dbContext.Find<BulletinBoard.Domain.Attachment.Attachment>(DataSeedHelper.TestAttachmentId);

            Assert.Null(advert);
        }
    }
}