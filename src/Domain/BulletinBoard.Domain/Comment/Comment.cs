namespace BulletinBoard.Domain.Comment;

using BulletinBoard.Domain.Attributes.MaxElementsAttribute;
using System.ComponentModel.DataAnnotations;
using Advert = Advert.Advert;
using Image = Image.Image;

/// <summary>
/// Комментарий к объявлению
/// </summary>
public class Comment
{
    /// <summary>
    /// Уникальный номер пользователя написавшего комментарий
    /// </summary>
    public required Guid UserId { get; init; }
    /// <summary>
    /// Никнейм пользователя написавшего комментарий
    /// </summary>
    public required string Nickname { get; init; }
    /// <summary>
    /// Текст комментария
    /// </summary>
    public required string Body { get; init; }
    /// <summary>
    /// Рейтинг комментарий от 1 до 5 баллов
    /// </summary>
    [Range(1, 5, ErrorMessage = "Балл должен быть от 1 до 5")]
    public required int Rating { get; init; }
    /// <summary>
    /// <see cref="Advert"/> на которое ссылается комментарий
    /// </summary>
    public required Advert Advert { get; init; }
    /// <summary>
    /// Вложения состоящие из <see cref="Image"/>
    /// </summary>
    [MaxElements(5)]
    public required IEnumerable<Image> Attachments { get; init; }
    /// <summary>
    /// Дата создания комментария
    /// </summary>
    public DateOnly Created { get; } = DateOnly.FromDateTime(DateTime.Now);
}
