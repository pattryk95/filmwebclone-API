using Microsoft.EntityFrameworkCore;

namespace filmwebclone_API.Entities
{
    public class FilmwebCloneContext : DbContext
    {
        public FilmwebCloneContext(DbContextOptions<FilmwebCloneContext> options) : base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var genres = modelBuilder.Entity<Genre>();
            genres.Property(g => g.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
