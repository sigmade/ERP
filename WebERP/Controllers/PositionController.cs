using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DBModels;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebERP.Models;

namespace WebERP.Controllers
{
    public class PositionController : Controller
    {
        

        ExportPeople exportPeople = new ExportPeople();


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExportExcel()
        {
            var position = exportPeople.Getrecord();
            var stream = new MemoryStream();
            var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");
            worksheet.Cells.LoadFromDataTable(position.Tables[0], true);
            package.Save();
            stream.Position = 0;
            string excelname = $"People-{DateTime.Now.ToString("yy-MM-dd-HH:mm")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);



        }
    }
}