using Microsoft.EntityFrameworkCore;


namespace Server
{
  //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))] // added this line for android ex: https://stackoverflow.com/questions/24981593/specified-key-was-too-long-max-key-length-is-767-bytes-mysql-error-in-entity-fr
  public class LibraryContext : DbContext
  {
    public DbSet<Book> Book { get; set; }

    public DbSet<Publisher> Publisher { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL("server=localhost;database=library;user=root;password=");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Publisher>(entity =>
      {
        entity.HasKey(e => e.ID);
        entity.Property(e => e.Name).IsRequired();
      });

      modelBuilder.Entity<Book>(entity =>
      {
        entity.HasKey(e => e.ISBN);
        entity.Property(e => e.Title).IsRequired();
        entity.HasOne(d => d.Publisher)
          .WithMany(p => p.Books);
      });

      // MySQL and EntityFrameworks migrations do not get along very well. Specifically, the MigrationHistory table has a primary key that is too large for MySQL
      // https://github.com/IdentityServer/IdentityServer2/issues/657
      // Exception on android was: MySql.Data.MySqlClient.MySqlException (0x80004005): Specified key was too long; max key length is 1000 bytes
      // Fixed by adding this line: (a string key is too long, we need to restrict the maxlength)
      modelBuilder.Entity<Book>(entity => entity.Property(m => m.ISBN).HasMaxLength(80));
    }
  }
}