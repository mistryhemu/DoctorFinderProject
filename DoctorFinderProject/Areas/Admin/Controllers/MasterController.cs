using DoctorFinderProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorFinderProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class MasterController : Controller
    {
        private DoctorfinderContext dc = null;
        public MasterController(DoctorfinderContext db)
        {
            this.dc = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection fc)
        {
            string email = fc["email"];
            string pass = fc["password"];

            var data = dc.Admintbls.Where(x => x.Email == email && x.Password == pass).FirstOrDefault();
            if (data != null)
            {
                //cookies
                //session

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.loginerror = "Invalid Email or password!";
            }
            return View();
        }
    }
}
