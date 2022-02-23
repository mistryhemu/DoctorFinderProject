using DoctorFinderProject.Controllers;
using DoctorFinderProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private IHttpContextAccessor _httpContextAccessor;
        CommonController objCommon = new CommonController();

        public DefaultController(DoctorfinderContext db, IWebHostEnvironment _environment, IHttpContextAccessor httpContextAccessor)
        {
            dc = db;
            env = _environment;
            _httpContextAccessor = httpContextAccessor;
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
                ////session
                _httpContextAccessor.HttpContext.Session.SetString("userId", Convert.ToString(data.PateintId));
                _httpContextAccessor.HttpContext.Session.SetString("userName",Convert.ToString(data.FirstName) + " " + Convert.ToString(data.LastName));
                _httpContextAccessor.HttpContext.Session.SetString("Profile", "../../Uploads/" + data.ProfileImage);
                _httpContextAccessor.HttpContext.Session.SetString("email", email);

                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.loginerror = "Invalid Email or password!";
            }
            return View();
        }
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("UserLogin");
        }

        public IActionResult UserRegistration()
        {
            var data = dc.Statetbls.ToList();

            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var item in data)
            {
                li.Add(new SelectListItem { Text = item.StateName, Value = item.StateId.ToString() });
            }
            ViewBag.states = li;
            return View();
        }
        [HttpPost]
        public IActionResult UserRegistration(IFormCollection fc)
        {
            Pateinttbl obj = new Pateinttbl();
            obj.FirstName = fc["firstname"];
            obj.LastName = fc["lastname"];
            obj.Email = fc["email"];
            obj.Password = fc["pass"];
            obj.Address = fc["address"];
            obj.StateId = Convert.ToInt32(fc["stateId"]);
            obj.CityId = Convert.ToInt32(fc["city"]);
            dc.Pateinttbls.Add(obj);
            dc.SaveChanges();
            return RedirectToAction("Home");
        }

        public JsonResult GetCitynameByStateId(int StateId)
        {
            var data = dc.Citytbls.Where(s => s.StateId == StateId);
            return Json(data.ToList());
        }
        public IActionResult Hospitalshow()
        {
            return View(dc.Hospitaltbls.ToList());
        }
        public IActionResult HospitalDetails(int HospitalId)
        {
            //string imageName = dc.Hospitaltbls.Find(HospitalId).ProfileImage;
            //ViewBag.imageName = imageName;
            var data = dc.Hospitaltbls
                .Include(x => x.State)
                .Include(d => d.City);
                

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
            
            obj.PateintId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            obj.AptStatus = (int)CommonController.AppointmentStatus.Waiting;
            dc.Appointmenttbls.Add(obj);
            dc.SaveChanges();
            return RedirectToAction("ManageAppointment");
        }
        public IActionResult ManageAppointment()
        {
            var data = dc.Appointmenttbls
                .Include(x => x.Hospital)
                .Include(d => d.Doctor)
                .Include(z => z.Pateint)
                .Where(t => t.PateintId == Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("userId"))).ToList();


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
            obj.PateintId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("userId")); 
            dc.Appointmenttbls.Update(obj);
            dc.SaveChanges();
            return RedirectToAction("ManageAppointment");
        }

        public JsonResult GetDoctorByHospitalId(int hospitalId)
        {
            var data = dc.Doctortbls.Where(s => s.HospitalId == hospitalId);
            return Json(data.ToList());
        }
        public IActionResult DeleteAppointment(int Id)
        {
            var data = dc.Appointmenttbls
               .Include(x => x.Hospital)
               .Include(d => d.Doctor)
               .Include(z => z.Pateint)
               .Where(t => t.PateintId == Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("userId"))).ToList();

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

