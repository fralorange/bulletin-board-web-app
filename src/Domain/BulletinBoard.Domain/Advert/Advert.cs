namespace BulletinBoard.Domain.Advert;

using Comment = Comment.Comment;
using User = User.User;
using Image = Image.Image;
using Category = Category.Category;
using BulletinBoard.Domain.Attributes.MaxElementsAttribute;

/// <summary>
/// Объявление
/// </summary>
public class Advert
{
    /// <summary>
    /// Уникальный номер объявления
    /// </summary>
    public required Guid Id { get; init; }
    /// <summary>
    /// Уникальный номер пользователя создавшего объявление
    /// </summary>
    public required Guid UserId { get; init; }
    /// <summary>
    /// <see cref="User"/> создавший объявление
    /// </summary>
    public required User User { get; init; }
    /// <summary>
    /// Название объявления
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// Описание объявления
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// Адрес объявления
    /// </summary>
    public required string Location { get; init; }
    /// <summary>
    /// Телефон пользователя создавшего объявление
    /// </summary>
    public required int Phone { get; init; }
    /// <summary>
    /// Электронная почта пользователя создавшего объявление
    /// </summary>
    public required string Email { get; init; }
    /// <summary>
    /// Цена
    /// </summary>
    public required decimal Cost { get; init; }
    /// <summary>
    /// <see cref="Comment"/> объявления
    /// </summary>
    public required IEnumerable<Comment> Comments { get; init; }
    /// <summary>
    /// <see cref="Image"/> объявления
    /// </summary>
    [MaxElements(10)]
    public required IEnumerable<Image> Images { get; init; }
    /// <summary>
    /// Категория объявления
    /// </summary>
    public required Category Category { get; init; }
    /// <summary>
    /// Дата создания объявления
    /// </summary>
    public DateOnly Created { get; } = DateOnly.FromDateTime(DateTime.Now);
}
