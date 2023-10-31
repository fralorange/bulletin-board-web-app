namespace BulletinBoard.Application.AppServices.Filtration.Ad.Filter
{
    /// <summary>
    /// Фильтр объявления.
    /// </summary>
    public class AdFilter
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Минимальная цена.
        /// </summary>
        public decimal? MinPrice { get; set; }
        /// <summary>
        /// Максимальная цена.
        /// </summary>
        public decimal? MaxPrice { get; set; }
    }
}
