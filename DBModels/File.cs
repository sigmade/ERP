using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
