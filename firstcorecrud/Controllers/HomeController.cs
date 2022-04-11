using firstcorecrud.db;
using firstcorecrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace firstcorecrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            coredbContext dbpbj = new coredbContext();
            var show = dbpbj.Coretablees.ToList();
            return View(show);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Privacy(student clobj)
        {
            coredbContext dbpbj = new coredbContext();
            Coretablee tbobj = new Coretablee();
            tbobj.Id = clobj.Id;
            tbobj.Name = clobj.Name;
            tbobj.Email = clobj.Email;
            tbobj.Phone = clobj.Phone;
            if (clobj.Id == 0) 
            {
                dbpbj.Coretablees.Add(tbobj);
                dbpbj.SaveChanges();
            }
            else
            {
                dbpbj.Update(tbobj);
               // dbpbj.Entry(clobj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbpbj.SaveChanges();
            }
            return RedirectToAction("Index");
            

        }



        public IActionResult delete(int id)
        {
            coredbContext dbpbj = new coredbContext();
            var del = dbpbj.Coretablees.Where(m => m.Id == id).First();
            dbpbj.Coretablees.Remove(del);
            dbpbj.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult edit(int id)
        {
            coredbContext dbpbj = new coredbContext();
            var edi = dbpbj.Coretablees.Where(m => m.Id == id).First();
            student obj = new student();
            obj.Id = edi.Id;
            obj.Name = edi.Name;
            obj.Email = edi.Email;
            obj.Phone = edi.Phone;
            return View("privacy",obj);
        }

            //    public IActionResult Privacy()
            //{
            //    return View();
            //}

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
