using DBModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DBModels;
using File = DBModels.File;

namespace WebERP.Controllers
{

    public class FilesController : Controller
    {
        private readonly MasterDBContext _context;
        IWebHostEnvironment _appEnvironment;


        public FilesController(MasterDBContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Create(int id, string nameDoc, IFormFile upFile)
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
                string path = "/images/employees/" + Convert.ToString(id) +  "-" + DateTime.Now.ToString("dd-MM-yy_H-mm-ss") + extension;
               
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await upFile.CopyToAsync(fileStream);
                }
                File file = new File { Name = name, Path = path, PersonId = id};
                _context.Files.Add(file);
                _context.SaveChanges();
            }


            return View();
        }


    }
}
