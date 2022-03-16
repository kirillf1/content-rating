using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Domain
{
    public class Author
    {
        public Author(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
