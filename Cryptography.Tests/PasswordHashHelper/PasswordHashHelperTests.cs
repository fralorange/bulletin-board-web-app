using BulletinBoard.Application.AppServices.Cryptography.Helpers;

namespace Cryptography.Tests.PasswordHelper
{
    public class PasswordHashHelperTests
    {
        [Fact]
        public void Test_GenerateDifferentHashes_ForSamePassword()
        {
            // Arrange
            var password = "Test_Password";

            // Act
            var (_, Hash1) = PasswordHashHelper.HashPassword(password);
            var (_, Hash2) = PasswordHashHelper.HashPassword(password);

            // Assert
            Assert.NotEqual(Hash1, Hash2);
        }
    }
}