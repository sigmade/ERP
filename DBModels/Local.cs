using System;
using System.Collections.Generic;

#nullable disable

namespace DBModels
{
    public partial class Local
    {
        public Local()
        {
            Worktimes = new HashSet<Worktime>();
        }

        public int LocalId { get; set; }
        public string LocalName { get; set; }

        public virtual ICollection<Worktime> Worktimes { get; set; }
    }
}
