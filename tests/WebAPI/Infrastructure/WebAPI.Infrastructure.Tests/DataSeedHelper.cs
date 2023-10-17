using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Cryptography.Helpers;
using BulletinBoard.Domain.Ad;
using BulletinBoard.Domain.Attachment;
using BulletinBoard.Domain.Category;
using BulletinBoard.Domain.Comment;
using BulletinBoard.Domain.User;
using BulletinBoard.Infrastructure.DataAccess;

namespace WebAPI.Infrastructure.Tests
{
    public static class DataSeedHelper
    {
        public static Guid TestAdvertId { get; set; }
        public static Guid TestCategoryId { get; set; }
        public static Guid TestCommentId { get; set; }
        public static Guid TestAttachmentId { get; set; }
        public static Guid TestUserId { get; set; }

        public static void InitializeDbForTests(BaseDbContext db)
        {
            var (Salt, Password) = PasswordHashHelper.HashPassword("test_password_1");

            var testUser = new User
            {
                Login = "test_user_1",
                Name = "test_username_1",
                HashedPassword = Password,
                Salt = Salt,
                Role = AuthRoles.Default,
            };

            db.Add(testUser);

            TestUserId = testUser.Id;

            var testCategory = new Category
            {
                CategoryName = "test_cat_1",
            };
            db.Add(testCategory);

            TestCategoryId = testCategory.Id;

            var testAdvert = new Ad
            {
                Title = "test_advert_name",
                Description = "test_desc",
                CategoryId = testCategory.Id,
                Price = 0,
                UserId = testUser.Id,
            };

            db.Add(testAdvert);
            
            TestAdvertId = testAdvert.Id;

            var testComment = new Comment
            {
                AdId = testAdvert.Id,
                Rating = 5,
                PublishedAt = DateTime.UtcNow,
                Content = "test_content_1",
                UserId = testUser.Id,
            };

            db.Add(testComment);

            TestCommentId = testComment.Id;

            var testAttachment = new Attachment
            {
                AdId = testAdvert.Id,
                Content = new byte[] { 13, 211, 17, 85, 255 },
            };

            db.Add(testAttachment);

            TestAttachmentId = testAttachment.Id;

            db.SaveChanges();

        }
    }
}
