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
            var hash1 = PasswordHashHelper.HashPassword(password);
            var hash2 = PasswordHashHelper.HashPassword(password);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
    }
}