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
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var genres = modelBuilder.Entity<Genre>();
            //genres.Property(g => g.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);

            modelBuilder.Entity<Genre>(eb =>
            {
                eb.Property(g => g.Name).HasColumnType("varchar(50)").IsRequired();

                eb.HasMany(g => g.Movies)
                .WithMany(m => m.Genres);
            });

            modelBuilder.Entity<Actor>(eb =>
            {

                eb.Property(a => a.FirstName).IsRequired().HasColumnType("varchar(120)");
                eb.Property(a => a.MiddleName).HasColumnType("varchar(120)");
                eb.Property(a => a.LastName).IsRequired().HasColumnType("varchar(120)");

                eb.HasMany(a => a.Movies)
                  .WithMany(m => m.Actors)
                  .UsingEntity<MoviesActors>(
                        a => a.HasOne(ma => ma.Movies)
                        .WithMany()
                        .HasForeignKey(ma => ma.MovieId),

                        a => a.HasOne(ma => ma.Actor)
                        .WithMany()
                        .HasForeignKey(ma => ma.ActorId),

                        ma =>
                        {
                            ma.HasKey(x => new { x.ActorId, x.MovieId });
                            ma.Property(x => x.Character).HasColumnType("varchar(75)");
                        });
            });

            modelBuilder.Entity<MovieTheater>(eb =>
                {
                    eb.Property(g => g.Name).HasColumnType("varchar(75)").IsRequired();
                    eb.HasMany(mt => mt.Movies)
                    .WithMany(m => m.MovieTheaters);
                });

            modelBuilder.Entity<Movie>(
                eb => eb.Property(g => g.Title).HasColumnType("varchar(75)").IsRequired()
                );

        }
    }
}
