using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace DBModels
{
    public partial class Person
    {
        

        public int PersonId { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateBirth { get; set; }
        public double? TaxNum { get; set; }
        public string SocNum { get; set; }
        public double? Phone { get; set; }
        public string Email { get; set; }
        public string RegionId { get; set; }
        public string ImageUrl { get; set; }
        public virtual List<Worktime> Worktimes { get; set; }

        public virtual List<File> Files { get; set; }
    }
}
