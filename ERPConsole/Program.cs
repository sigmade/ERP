using System;
using System.Collections.Generic;
using System.Linq;
using DBModels;

namespace ERPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MasterDBContext db = new MasterDBContext())
            {
                var users = db.People.ToList();
                var result = from c in users where c.RegionId == "60" select c;
                //foreach (var u in result)
                //{
                //    Console.WriteLine($"User {u.FullName}");
                //}
                
                var month = (from hours in db.Worktimes
                             where hours.PersonId == 37
                             group hours.Hours by new { hours.Date.Month } into g
                             select new
                             {
                                 Months = g.Key.Month,
                                 SummHourse = g.Sum()
                             }).ToList();
                foreach (var u in month)
                {
                    Console.WriteLine($"Months {u.Months} Hourse: {u.SummHourse}");
                }
            }
        }
    }
}
