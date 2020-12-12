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
            var month = _context.Worktimes;
            var summ = _context.Worktimes
                .Sum(w => w.Hours);
            ViewData["SummHourse"] = summ;
            return View(month);

        }


    }
}