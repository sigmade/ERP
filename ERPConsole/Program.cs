using System;
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
                foreach (var u in result)
                {
                    Console.WriteLine($"User {u.FullName}");
                }
            }
        }
    }
}
