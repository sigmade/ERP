using System;
using System.Collections.Generic;

#nullable disable

namespace DBModels
{
    public partial class Person
    {
        public Person()
        {
            Worktimes = new HashSet<Worktime>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string Male { get; set; }
        public DateTime DateBirth { get; set; }
        public double TaxNum { get; set; }
        public string SocNum { get; set; }
        public double PasportNum { get; set; }
        public double Phone { get; set; }
        public string Email { get; set; }
        public string RegionId { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Worktime> Worktimes { get; set; }
    }
}
