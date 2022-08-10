using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmwebclone_API.Entities
{
    public class MovieTheater
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}
