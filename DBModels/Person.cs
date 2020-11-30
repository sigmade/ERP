using System;
using System.Collections.Generic;


namespace DBModels
{
    public partial class Person
    {
        

        public int PersonId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateBirth { get; set; }
        public double? TaxNum { get; set; }
        public string SocNum { get; set; }
        public double? Phone { get; set; }
        public string Email { get; set; }
        public string RegionId { get; set; }

        public virtual List<Worktime> Worktimes { get; set; }
    }
}
