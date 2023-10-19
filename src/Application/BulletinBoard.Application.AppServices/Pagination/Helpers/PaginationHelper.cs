namespace BulletinBoard.Application.AppServices.Pagination.Helpers
{
    /// <summary>
    /// Хелпер для выполнения пагинации.
    /// </summary>
    public static class PaginationHelper<TModel>
    {
        /// <summary>
        /// Разбиение коллекции моделей на страницы.
        /// </summary>
        /// <param name="modelCollection">Коллекция моделей.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns></returns>
        public static async Task<IReadOnlyCollection<TModel>> SplitByPages(Task<IReadOnlyCollection<TModel>> modelCollection, int pageSize, int pageIndex) 
        {
            return (await modelCollection)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
