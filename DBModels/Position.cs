using System;
using System.Collections.Generic;


namespace DBModels
{
    public partial class Position
    {
       

        public int PositionId { get; set; }
        public string PositionName { get; set; }

        public virtual List<Worktime> Worktimes { get; set; }
    }
}
