using Microsoft.EntityFrameworkCore;

namespace filmwebclone_API.Entities
{
    public class FilmwebCloneContext : DbContext
    {
        public FilmwebCloneContext(DbContextOptions<FilmwebCloneContext> options) : base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var genres = modelBuilder.Entity<Genre>();
            //genres.Property(g => g.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);

            modelBuilder.Entity<Genre>(
                eb => eb.Property(g => g.Name).HasColumnType("varchar(50)").IsRequired()
                );
            modelBuilder.Entity<Actor>(
                eb => eb.Property(a => a.FirstName).IsRequired().HasColumnType("varchar(120)")
                );
            modelBuilder.Entity<Actor>(
                eb => eb.Property(a => a.MiddleName).HasColumnType("varchar(120)")
                );
            modelBuilder.Entity<Actor>(
                eb => eb.Property(a => a.LastName).IsRequired().HasColumnType("varchar(120)")
                );
        }
    }
}
