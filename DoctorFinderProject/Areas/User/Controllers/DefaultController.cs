using DoctorFinderProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorFinderProject.Areas.User.Controllers
{
    [Area("User")]
    public class DefaultController : Controller
    {
        private DoctorfinderContext dc = null;
        private IWebHostEnvironment env;
        public DefaultController(DoctorfinderContext db, IWebHostEnvironment _environment)
        {
            dc = db;
            env = _environment;
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(IFormCollection fc)
        {
            string email = fc["email"];
            string pass = fc["password"];

            var data = dc.Pateinttbls.Where(x => x.Email == email && x.Password == pass).FirstOrDefault();
            if (data != null)
            {
                //cookies
                //session
                //_httpContextAccessor.HttpContext.Session.SetString("Email", "hemu11298@gmail.com");
                //_httpContextAccessor.HttpContext.Session.SetString("password", "1234");
                //ViewBag.sessionid = _httpContextAccessor.HttpContext.Session.Id;


                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.loginerror = "Invalid Email or password!";
            }
            return View();
        }
       public IActionResult Hospitalshow()
        {
            return View(dc.Hospitaltbls.ToList());
        }
        public IActionResult HospitalDetails(int HospitalId)
        {
            //string imageName = dc.Hospitaltbls.Find(HospitalId).ProfileImage;
            //ViewBag.imageName = imageName;
            return View(dc.Hospitaltbls.Find(HospitalId));
        }
        public IActionResult Doctorshow()
        {
            return View(dc.Doctortbls.ToList());
        }
        public IActionResult DoctorDetails(int DoctorId)
        {
            //string imageName = dc.Hospitaltbls.Find(HospitalId).ProfileImage;
            //ViewBag.imageName = imageName;
            return View(dc.Doctortbls.Find(DoctorId));
        }
    }
    }

