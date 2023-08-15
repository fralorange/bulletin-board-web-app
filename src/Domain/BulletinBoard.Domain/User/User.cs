namespace BulletinBoard.Domain.User;

using Advert = Advert.Advert;
using Comment = Comment.Comment;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный номер
    /// </summary>
    public required Guid Id { get; init; }
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// Электронная почта пользователя
    /// </summary>
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
    public required string Email { get; init; }
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required int Password { get; init; }
    /// <summary>
    /// Телефон пользователя (необязательное поле)
    /// </summary>
    public required int Phone { get; set; } = -1;
    /// <summary>
    /// <see cref="Advert"/> пользователя
    /// </summary>
    public required IEnumerable<Advert> Adverts { get; init; } = Enumerable.Empty<Advert>();
    /// <summary>
    /// <see cref="Comment"/> пользователя
    /// </summary>
    public required IEnumerable<Comment> Comments { get; init; } = Enumerable.Empty<Comment>();
}

