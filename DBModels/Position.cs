using System;
using System.Collections.Generic;

#nullable disable

namespace DBModels
{
    public partial class Position
    {
        public Position()
        {
            Worktimes = new HashSet<Worktime>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; }

        public virtual ICollection<Worktime> Worktimes { get; set; }
    }
}
