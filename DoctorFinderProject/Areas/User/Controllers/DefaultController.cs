using DoctorFinderProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult AddApointment()
        {
            var data = dc.Hospitaltbls.ToList();

            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var item in data)
            {
                li.Add(new SelectListItem { Text = item.HospitalName, Value = item.HospitalId.ToString() });
            }
            ViewBag.hospitals = li;
            return View();

        }
        [HttpPost]
        public IActionResult AddApointment(Appointmenttbl obj)
        {
            obj.PateintId = 1;

            dc.Appointmenttbls.Add(obj);
            dc.SaveChanges();
            return RedirectToAction("Home");
        }
        public IActionResult ManageAppointment()
        {
            var data = dc.Appointmenttbls.Where(t => t.PateintId == 1).ToList();

            
            //foreach(var item in data)
            //{
            //    //item.Doctor.FirstName = dc.Doctortbls.Find(item.DoctorId).FirstName;
            //}
            return View(data);
        }
        public IActionResult EditAppointment(int AptId)
        {
            var data = dc.Hospitaltbls.ToList();

            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var item in data)
            {
                li.Add(new SelectListItem { Text = item.HospitalName, Value = item.HospitalId.ToString() });
            }
            ViewBag.hospitals = li;
           
            return View(dc.Appointmenttbls.Find(AptId));
        }

        [HttpPost]
        public IActionResult EditAppointment(Appointmenttbl obj)
        {
            obj.PateintId = 1;
            dc.Appointmenttbls.Update(obj);
            dc.SaveChanges();
            return RedirectToAction("ManageAppointment");
        }
        public IActionResult DeleteAppointment(int Id)
        {
            return View(dc.Appointmenttbls.Find(Id));
        }
        [HttpPost]
        [ActionName("DeleteAppointment")]
        public IActionResult DeleteAppointmentRec(int Id)
        {
            dc.Appointmenttbls.Remove(dc.Appointmenttbls.Find(Id));
            dc.SaveChanges();
            return RedirectToAction("ManageAppointment");
        }
    }
    }

