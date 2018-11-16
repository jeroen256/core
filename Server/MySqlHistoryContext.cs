// MySQL and EntityFrameworks migrations do not get along very well. Specifically, the MigrationHistory table has a primary key that is too large for MySQL
// https://github.com/IdentityServer/IdentityServer2/issues/657
// Exception on android was: MySql.Data.MySqlClient.MySqlException (0x80004005): Specified key was too long; max key length is 1000 bytes
// Tl-dr; If you want to use MySQL, you'll have to add these classes to your project:
using Microsoft.EntityFrameworkCore;

namespace Server
{
  public class MySqlHistoryContext : HistoryContext
    {
      public MySqlHistoryContext(
        DbConnection existingConnection,
        string defaultSchema)
      : base(existingConnection, defaultSchema)
      {
      }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey).HasMaxLength(200).IsRequired();
      }
    }

  public class MySqlConfiguration : DbConfiguration
    {
      public MySqlConfiguration()
      {
        SetHistoryContext(
        "MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
      }
    }
}