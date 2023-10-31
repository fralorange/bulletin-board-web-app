namespace BulletinBoard.Application.AppServices.Filtration.Comment.Filter
{
    /// <summary>
    /// Фильтр комментария.
    /// </summary>
    public class CommentFilter
    {
        /// <summary>
        /// Минимальный рейтинг.
        /// </summary>
        public int? MinRating { get; set; }

        /// <summary>
        /// Максимальный рейтинг.
        /// </summary>
        public int? MaxRating { get; set; }
    }
}
