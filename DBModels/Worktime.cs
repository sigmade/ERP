using System;
using System.Collections.Generic;

#nullable disable

namespace DBModels
{
    public partial class Worktime
    {
        public int WorktimeId { get; set; }
        public int PersonId { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int LocalId { get; set; }
        public int PositionId { get; set; }

        public virtual Local Local { get; set; }
        public virtual Person Person { get; set; }
        public virtual Position Position { get; set; }
    }
}
