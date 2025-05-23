using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Trial
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string? Occupation { get; set; }
    }
}
