using BulletinBoard.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Hosts.DbMigrator
{
    public class MigrationDbContext : BaseDbContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options) { }
    }
}
