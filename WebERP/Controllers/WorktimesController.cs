using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBModels;

namespace WebERP.Controllers
{
    public class WorktimesController : Controller
    {
        private readonly MasterDBContext _context;

        public WorktimesController(MasterDBContext context)
        {
            _context = context;
        }

        // GET: Worktimes
        public async Task<IActionResult> Index(int? pageNumber)
        {
          
            ViewData["PageNumber"] = pageNumber;
            var worktimes = _context.Worktimes.Include(w => w.Local).Include(w => w.Person).Include(w => w.Position);
            int pageSize = 20;
            return View(await PaginatedList<Worktime>.CreateAsync(worktimes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Worktimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worktime = await _context.Worktimes
                .Include(w => w.Local)
                .Include(w => w.Person)
                .Include(w => w.Position)
                .FirstOrDefaultAsync(m => m.WorktimeId == id);
            if (worktime == null)
            {
                return NotFound();
            }

            return View(worktime);
        }

        // GET: Worktimes/Create
        public IActionResult Create()
        {
            ViewData["LocalId"] = new SelectList(_context.Locals, "LocalId", "LocalName");
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId");
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            return View();
        }

        // POST: Worktimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorktimeId,PersonId,Date,Hours,LocalId,PositionId")] Worktime worktime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worktime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalId"] = new SelectList(_context.Locals, "LocalId", "LocalName", worktime.LocalId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", worktime.PersonId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", worktime.PositionId);
            return View(worktime);
        }

        // GET: Worktimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worktime = await _context.Worktimes.FindAsync(id);
            if (worktime == null)
            {
                return NotFound();
            }
            ViewData["LocalId"] = new SelectList(_context.Locals, "LocalId", "LocalName", worktime.LocalId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", worktime.PersonId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", worktime.PositionId);
            return View(worktime);
        }

        // POST: Worktimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorktimeId,PersonId,Date,Hours,LocalId,PositionId")] Worktime worktime)
        {
            if (id != worktime.WorktimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worktime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorktimeExists(worktime.WorktimeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalId"] = new SelectList(_context.Locals, "LocalId", "LocalName", worktime.LocalId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", worktime.PersonId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", worktime.PositionId);
            return View(worktime);
        }

        // GET: Worktimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worktime = await _context.Worktimes
                .Include(w => w.Local)
                .Include(w => w.Person)
                .Include(w => w.Position)
                .FirstOrDefaultAsync(m => m.WorktimeId == id);
            if (worktime == null)
            {
                return NotFound();
            }

            return View(worktime);
        }

        // POST: Worktimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worktime = await _context.Worktimes.FindAsync(id);
            _context.Worktimes.Remove(worktime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorktimeExists(int id)
        {
            return _context.Worktimes.Any(e => e.WorktimeId == id);
        }
    }
}
