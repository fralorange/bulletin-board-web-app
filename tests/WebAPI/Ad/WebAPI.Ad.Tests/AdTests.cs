using BulletinBoard.Contracts.Ad;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using WebAPI.Infrastructure.Tests;
using WebAPI.Infrastructure.Tests.Factories;

namespace WebAPI.Ad.Tests
{
    public class AdTests : IClassFixture<BulletinBoardWebApplicationFactory>
    {
        private readonly BulletinBoardWebApplicationFactory _webApplicationFactory;

        public AdTests(BulletinBoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async void Test_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestAdvertId;

            // Act
            var response = await httpClient.GetAsync($"ad/get-by-id?id={id}");

            // Assert

            Assert.NotNull(response);

            var result = await response.Content.ReadFromJsonAsync<AdDto>();

            Assert.NotNull(result);

            Assert.Equal("test_advert_name", result!.Title);
            Assert.Equal("test_desc", result.Description);
            Assert.Equal("0", result.Price.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_GetByIdAsync_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/ad/get-by-id?id=00000000-0000-0000-0000-000000000000");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test_Ad_Create_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            CreateAdDto model = new()
            {
                Title = "test_name",
                Description = "test_description",
                CategoryId = DataSeedHelper.TestCategoryId,
                Price = 0
            };

            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("ad", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Получить id созданной сущности
            var id = (await response.Content.ReadAsStringAsync()).Trim('"');

            // Получить созданную сущность
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var advert = dbContext.Find<BulletinBoard.Domain.Ad.Ad>(Guid.Parse(id!));

            Assert.NotNull(advert);

            Assert.Equal(model.Title, advert!.Title);
            Assert.Equal(model.Description, advert.Description);
            Assert.Equal(model.Price, advert.Price);
        }

        [Fact]
        public async Task Test_Ad_Update_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            UpdateAdDto model = new()
            {
                Title = "new_test_name",
                Description = "new_test_description",
                CategoryId = DataSeedHelper.TestCategoryId,
                Price = 10
            };

            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/ad/{DataSeedHelper.TestAdvertId}", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Получить обновлённую сущность
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var advert = dbContext.Find<BulletinBoard.Domain.Ad.Ad>(DataSeedHelper.TestAdvertId);

            Assert.NotNull(advert);

            Assert.Equal(model.Title, advert!.Title);
            Assert.Equal(model.Description, advert.Description);
            Assert.Equal(model.Price, advert.Price);
        }

        [Fact]
        public async Task Test_Ad_Delete_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"/ad/{DataSeedHelper.TestAdvertId}");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Проверить, что сущность была удалена
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var advert = dbContext.Find<BulletinBoard.Domain.Ad.Ad>(DataSeedHelper.TestAdvertId);

            Assert.Null(advert);
        }
    }
}