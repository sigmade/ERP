using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebERP.Controllers
{
    public class ReportsController : Controller
    {
        private readonly MasterDBContext _context;

        public ReportsController(MasterDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var person = _context.People
                .Include(w => w.Worktimes)
                    .ThenInclude(l => l.Local)
                .Include(p => p.Worktimes)
                    .ThenInclude(l => l.Position)
                 .First();
            var summ = _context.Worktimes
                .Sum(w => w.Hours);

            //var month = (from hours in _context.Worktimes
            //             group hours.Hours by new { hours.Date.Month } into g
            //             select new
            //             {
            //                 Months = g.Key.Month,
            //                 SummHourse = g.Sum()
            //             });

            ViewData["SummHourse"] = summ;

            return View(person);
        }
    }
}