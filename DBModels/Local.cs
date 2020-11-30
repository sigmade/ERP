using System;
using System.Collections.Generic;


namespace DBModels
{
    public partial class Local
    {
      
        public int LocalId { get; set; }
        public string LocalName { get; set; }

        public virtual List<Worktime> Worktimes { get; set; }
    }
}
