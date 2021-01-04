using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBModels;
using OfficeOpenXml;
using WebERP.Models;
using System.IO;
using System.Data;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using File = DBModels.File;
using Microsoft.AspNetCore.Hosting;

namespace WebERP.Controllers
{
    public class PeopleController : Controller
    {
        private readonly MasterDBContext _context;
        private readonly HostingEnvironment _hostingEnvironment;
        IWebHostEnvironment _appEnvironment;

        public PeopleController(MasterDBContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        ExcelExport exportPeople = new ExcelExport();

        // GET: People
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "id";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PageNumber"] = pageNumber;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var people = from s in _context.People
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                // people = people.Where(s => s.FullName.Contains(searchString) || s.PersonId == Convert.ToInt32(searchString));
                people = people.Where(s => s.FullName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    people = people.OrderByDescending(s => s.FullName);
                    break;
                case "id":
                    people = people.OrderBy(s => s.PersonId);
                    break;
                case "id_desc":
                    people = people.OrderByDescending(s => s.PersonId);
                    break;

                default:
                    people = people.OrderBy(s => s.FullName);
                    break;
            }
            int pageSize = 20;
            return View(await PaginatedList<Person>.CreateAsync(people.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Export()
        {
            var position = exportPeople.Getquery("SELECT TOP(300) * FROM Person");
            var stream = new MemoryStream();
            var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");
            worksheet.Cells["K5"].Value = "Список сотрудников";
            worksheet.Cells.LoadFromDataTable(position.Tables[0], true);
            package.Save();
            stream.Position = 0;
            string excelname = $"People-{DateTime.Now.ToString("yy-MM-dd-HH:mm")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);

        }

       

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        // GET: People/Summary/5
        public async Task<IActionResult> Summary(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(w => w.Worktimes)
                    .ThenInclude(l => l.Local)
                .Include(p => p.Worktimes)
                    .ThenInclude(l => l.Position)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }
            var summ = _context.Worktimes.Where(p => p.PersonId == id)
                .Sum(w => w.Hours);
              
            ViewData["SummHourse"] = summ;

            return View(person);
        }

        

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,DateBirth,TaxNum,SocNum,Phone,Email,RegionId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FullName,DateBirth,TaxNum,SocNum,Phone,Email,RegionId")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }
        [HttpGet]
        public IActionResult AddFile()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AddFile(int id, string nameDoc, IFormFile upFile)
        {
            //TODO: Validation file
            string name = nameDoc;
            var extension = Path.GetExtension(upFile.FileName);

            if (id == 0)
            {
                return NotFound();
            }

            if (upFile != null)
            {
                string path = "/images/employees/" + Convert.ToString(id) + "-" + DateTime.Now.ToString("dd-MM-yy_H-mm-ss") + extension;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await upFile.CopyToAsync(fileStream);
                }
                File file = new File { Name = name, Path = path, PersonId = id, DateJoined = DateTime.UtcNow };
                _context.Files.Add(file);
                _context.SaveChanges();
            }
            return RedirectToAction("EmployeeFiles",  new { id = 83});
        }
        public IActionResult EmployeeFiles(int? id)
        {
            if (id == null)
            {
                return View();
            }


            var filesList = _context.Files.Where(p => p.PersonId == id);
            var fullname = _context.People.Where(p => p.PersonId == id)
                .FirstOrDefault(m => m.PersonId == id);
            ViewData["PersonId"] = id;
            ViewData["Person"] = fullname.FullName;

            return View(filesList);
        }
    }
}
