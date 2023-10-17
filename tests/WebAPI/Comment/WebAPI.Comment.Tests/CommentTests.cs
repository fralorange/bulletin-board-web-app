using BulletinBoard.Contracts.Comment;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using WebAPI.Infrastructure.Tests;
using WebAPI.Infrastructure.Tests.Factories;

namespace WebAPI.Comment.Tests
{
    public class CommentTests : IClassFixture<BulletinBoardWebApplicationFactory>
    {
        private readonly BulletinBoardWebApplicationFactory _webApplicationFactory;
        public CommentTests(BulletinBoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async void Test_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestCommentId;

            // Act
            var response = await httpClient.GetAsync($"comment/get-by-id?id={id}");

            // Assert

            Assert.NotNull(response);

            var result = await response.Content.ReadFromJsonAsync<CommentDto>();

            Assert.NotNull(result);

            Assert.Equal("test_content_1", result!.Content);
            Assert.Equal("5", result.Rating.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_GetByIdAsync_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/comment/get-by-id?id=00000000-0000-0000-0000-000000000000");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test_Comment_Create_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            CreateCommentDto model = new()
            {
                Content = "test_content_1",
                Rating = 5,
                AdId = DataSeedHelper.TestAdvertId,
            };

            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("comment", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Получить id созданной сущности
            var id = (await response.Content.ReadAsStringAsync()).Trim('"');

            // Получить созданную сущность
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var comment = dbContext.Find<BulletinBoard.Domain.Comment.Comment>(Guid.Parse(id!));

            Assert.NotNull(comment);

            Assert.Equal(model.Content, comment!.Content);
            Assert.Equal(model.Rating, comment.Rating);
        }

        [Fact]
        public async Task Test_Comment_Update_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            UpdateCommentDto model = new()
            {
                Content = "new_test_content_1",
                Rating = 4,
            };

            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/comment/{DataSeedHelper.TestCommentId}", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Получить обновлённую сущность
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var comment = dbContext.Find<BulletinBoard.Domain.Comment.Comment>(DataSeedHelper.TestCommentId);

            Assert.NotNull(comment);

            Assert.Equal(model.Content, comment!.Content);
            Assert.Equal(model.Rating, comment.Rating);
        }

        [Fact]
        public async Task Test_Comment_Delete_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"/comment/{DataSeedHelper.TestCommentId}");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Проверить, что сущность была удалена
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var comment = dbContext.Find<BulletinBoard.Domain.Comment.Comment>(DataSeedHelper.TestCommentId);

            Assert.Null(comment);
        }
    }
}