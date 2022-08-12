using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Entities
{
    public class MoviesActors
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public int MovieId { get; set; }
        public Movie Movies { get; set; }

        public string Character { get; set; }
        public int Order { get; set; }
    }
}
