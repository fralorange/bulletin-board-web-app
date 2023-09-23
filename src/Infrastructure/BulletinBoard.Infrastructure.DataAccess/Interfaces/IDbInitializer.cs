namespace BulletinBoard.Infrastructure.DataAccess.Interfaces
{
    /// <summary>
    /// Инициализатор БД.
    /// </summary>
    public interface IDbInitializer
    {
        /// <summary>
        /// Инициализировать БД.
        /// </summary>
        void InitializeDb();
    }
}
