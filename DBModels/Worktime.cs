using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBModels
{
    public partial class Worktime
    {
        public int WorktimeId { get; set; }
        public int PersonId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int LocalId { get; set; }
        public int PositionId { get; set; }

        public virtual Local Local { get; set; }
        public virtual Person Person { get; set; }
        public virtual Position Position { get; set; }
    }
}
