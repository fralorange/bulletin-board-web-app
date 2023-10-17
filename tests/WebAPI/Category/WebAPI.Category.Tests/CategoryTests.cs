using BulletinBoard.Contracts.Category;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using WebAPI.Infrastructure.Tests;
using WebAPI.Infrastructure.Tests.Factories;

namespace WebAPI.Category.Tests
{
    public class CategoryTests : IClassFixture<BulletinBoardWebApplicationFactory>
    {
        private readonly BulletinBoardWebApplicationFactory _webApplicationFactory;

        public CategoryTests(BulletinBoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async void Test_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestCategoryId;

            // Act
            var response = await httpClient.GetAsync($"category/get-by-id?id={id}");

            // Assert

            Assert.NotNull(response);

            var result = await response.Content.ReadFromJsonAsync<CategoryDto>();

            Assert.NotNull(result);

            Assert.Equal("test_cat_1", result!.CategoryName);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_GetByIdAsync_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/category/get-by-id?id=00000000-0000-0000-0000-000000000000");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test_Category_Create_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            CreateCategoryDto model = new()
            {
                CategoryName = "test_cat_1",
            };

            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("category", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Получить id созданной сущности
            var id = (await response.Content.ReadAsStringAsync()).Trim('"');

            // Получить созданную сущность
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var category = dbContext.Find<BulletinBoard.Domain.Category.Category>(Guid.Parse(id!));

            Assert.NotNull(category);

            Assert.Equal(model.CategoryName, category!.CategoryName);
        }

        [Fact]
        public async Task Test_Category_Update_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            UpdateCategoryDto model = new()
            {
                CategoryName = "new_test_cat_1",
            };

            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/category/{DataSeedHelper.TestCategoryId}", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Получить обновлённую сущность
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var category = dbContext.Find<BulletinBoard.Domain.Category.Category>(DataSeedHelper.TestCategoryId);

            Assert.NotNull(category);

            Assert.Equal(model.CategoryName, category!.CategoryName);
        }

        [Fact]
        public async Task Test_Category_Delete_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"/category/{DataSeedHelper.TestCategoryId}");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Проверить, что сущность была удалена
            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var category = dbContext.Find<BulletinBoard.Domain.Category.Category>(DataSeedHelper.TestCategoryId);

            Assert.Null(category);
        }
    }
}