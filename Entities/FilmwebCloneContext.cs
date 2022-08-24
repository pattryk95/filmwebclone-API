using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace filmwebclone_API.Entities
{
    public class FilmwebCloneContext : IdentityDbContext
    {
        public FilmwebCloneContext(DbContextOptions<FilmwebCloneContext> options) : base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MovieTheatersMovies> MovieTheatersMovies { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var genres = modelBuilder.Entity<Genre>();
            //genres.Property(g => g.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);

            modelBuilder.Entity<Genre>(eb =>
            {
                eb.Property(g => g.Name).HasColumnType("varchar(50)").IsRequired();

            });

            modelBuilder.Entity<Actor>(eb =>
            {

                eb.Property(a => a.FirstName).IsRequired().HasColumnType("varchar(120)");
                eb.Property(a => a.MiddleName).HasColumnType("varchar(120)");
                eb.Property(a => a.LastName).IsRequired().HasColumnType("varchar(120)");

            });

            modelBuilder.Entity<MovieTheater>(eb =>
                {
                    eb.Property(g => g.Name).HasColumnType("varchar(75)").IsRequired();

                });

            modelBuilder.Entity<Movie>(
                eb => eb.Property(g => g.Title).HasColumnType("varchar(75)").IsRequired()
                );

            modelBuilder.Entity<MoviesActors>(eb =>
            {
                eb.Property(ma => ma.Character).HasColumnType("varchar(75)");
                eb.HasKey(c => new { c.ActorId, c.MovieId });
            });

            modelBuilder.Entity<MoviesGenres>().HasKey(c => new { c.GenreId, c.MovieId });
            modelBuilder.Entity<MovieTheatersMovies>().HasKey(c=> new {c.MovieTheaterId, c.MovieId});

            base.OnModelCreating(modelBuilder);

        }
    }
}
