using filmwebclone_API.Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace filmwebclone_API.Entities
{
    public class Genre 
    {
        public int Id { get; set; }
        [FirstLetterUppercase]
        public string Name { get; set; }

        public List<MoviesGenres> MoviesGenres { get; set; } = new List<MoviesGenres>();
    }
}
