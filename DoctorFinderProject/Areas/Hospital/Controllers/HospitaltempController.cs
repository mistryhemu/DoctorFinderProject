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

namespace DoctorFinderProject.Areas.Hospital.Controllers
{
    [Area("Hospital")]
    public class HospitaltempController : Controller
    {
        private DoctorfinderContext dc = null;
        private IWebHostEnvironment env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HospitaltempController(DoctorfinderContext db, IWebHostEnvironment _environment, IHttpContextAccessor httpContextAccessor)
        {
            dc = db;
            env = _environment;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Hospital()
        {
            return View();
        }
        public IActionResult HospitalLogin()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("HospitalLogin");
        }
        [HttpPost]
        public IActionResult HospitalLogin(IFormCollection fc)
        {
            string email = fc["email"];
            string pass = fc["password"];

            var data = dc.Hospitaltbls.Where(x => x.Email == email && x.Password == pass).FirstOrDefault();
            if (data != null)
            {
                //cookies
                //session
                _httpContextAccessor.HttpContext.Session.SetString("Profile", "../../Uploads/" + data.ProfileImage);
                _httpContextAccessor.HttpContext.Session.SetString("email", email);

                return RedirectToAction("Hospital");
            }
            else
            {
                ViewBag.loginerror = "Invalid Email or password!";
            }
            return View();
        }
        public IActionResult ManageHospital()
        {
            return View(dc.Hospitaltbls.ToList());
        }
        public IActionResult AddHospital()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddHospital(Hospitaltbl obj, IFormCollection fc, List<IFormFile> postedFiles)
        {
            string wwwpath = env.WebRootPath;
            string contentPath = env.ContentRootPath;

            string path = Path.Combine(env.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(filename);
                    obj.ProfileImage = filename;
                }
            }
            obj.CityId = 1;
            obj.StateId = 1;
            dc.Hospitaltbls.Add(obj);
            dc.SaveChanges();



            return RedirectToAction("ManageHospital");
        }

        public IActionResult Edit(int HospitalId)
        {
            var data = dc.Statetbls.ToList();
            
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var item in data)
            {
                li.Add(new SelectListItem { Text = item.StateName, Value = item.StateId.ToString() });
            }
            ViewBag.states = li;
            return View(dc.Hospitaltbls.Find(HospitalId));
        }
        [HttpPost]
        public IActionResult Edit(Hospitaltbl obj, List<IFormFile> postedFiles)
        {
            string wwwpath = env.WebRootPath;
            string contentPath = env.ContentRootPath;

            string path = Path.Combine(env.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(filename);
                    obj.ProfileImage = filename;
                }
            }
            obj.CityId = 1;
            obj.StateId = 1;
            dc.Hospitaltbls.Update(obj);
            dc.SaveChanges();
            return RedirectToAction("ManageHospital");
        }
        public JsonResult GetCityByStateId(int StateId)
        {
            var data = dc.Citytbls.Where(s => s.StateId == StateId);
            return Json(data.ToList());
        }
        public IActionResult Delete(int Id)
        {
            return View(dc.Hospitaltbls.Find(Id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteRec(int Id)
        {
            dc.Hospitaltbls.Remove(dc.Hospitaltbls.Find(Id));
            dc.SaveChanges();
            return RedirectToAction("ManageHospital");
        }
        public IActionResult ManagePatient()
        {
            return View(dc.Pateinttbls.ToList());
        }


        public IActionResult AddPatient()
        {
            //Add Patient 
            return View();
        }
        [HttpPost]
        public IActionResult AddPatient(Pateinttbl obj, IFormCollection fc, List<IFormFile> postedFiles)
        {
            string wwwpath = env.WebRootPath;
            string contentPath = env.ContentRootPath;

            string path = Path.Combine(env.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(filename);
                    obj.ProfileImage = filename;
                }
            }
            obj.CityId = 1;
            obj.StateId = 1;
            dc.Pateinttbls.Add(obj);
            dc.SaveChanges();



            return RedirectToAction("ManagePatient");
        }
        public IActionResult EditPatient(int PateintId)
        {
            var data = dc.Statetbls.ToList();
           // string existingImage = dc.Pateinttbls.Find(PateintId).ProfileImage;
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var item in data)
            {
                li.Add(new SelectListItem { Text = item.StateName, Value = item.StateId.ToString() });
            }

            //var cityData = dc.Citytbls.Where(t => t.StateId == selectedState).ToList();
            //List<SelectListItem> liCity = new List<SelectListItem>();
            //foreach (var item in cityData)
            //{
            //    liCity.Add(new SelectListItem { Text = item.CityName, Value = item.CityName.ToString() });
            //}

            ViewBag.states = li;
            //ViewBag.cities = liCity;
            return View(dc.Pateinttbls.Find(PateintId));
        }
        [HttpPost]
        public IActionResult EditPatient(Pateinttbl obj, List<IFormFile> postedFiles)
        {
            string wwwpath = env.WebRootPath;
            string contentPath = env.ContentRootPath;

            string path = Path.Combine(env.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();


            foreach (IFormFile postedFile in postedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(filename);
                    obj.ProfileImage = filename;
                }
            }


            obj.CityId = 1;
            obj.StateId = 1;
            dc.Pateinttbls.Update(obj);
            dc.SaveChanges();
            return RedirectToAction("ManagePatient");
        }
        public JsonResult GetCitiesByStateId(int StateId)
        {
            var data = dc.Citytbls.Where(s => s.StateId == StateId);
            return Json(data.ToList());
        }

        public IActionResult DeletePatient(int Id)
        {
            return View(dc.Pateinttbls.Find(Id));
        }
        [HttpPost]
        [ActionName("DeletePatient")]
        public IActionResult DeletePatientRec(int Id)
        {
            dc.Pateinttbls.Remove(dc.Pateinttbls.Find(Id));
            dc.SaveChanges();
            return RedirectToAction("ManagePatient");
        }
        public IActionResult ManageDoctor()
        {
            return View(dc.Doctortbls.ToList());
        }
        public IActionResult AddDoctor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDoctor(Doctortbl obj, IFormCollection fc, List<IFormFile> postedFiles)
        {
            string wwwpath = env.WebRootPath;
            string contentPath = env.ContentRootPath;

            string path = Path.Combine(env.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(filename);
                    obj.ProfileImage = filename;
                }
            }
            //obj.CityId = 1;
            //obj.StateId = 1;
            obj.HospitalId = 5;
            dc.Doctortbls.Add(obj);
            dc.SaveChanges();



            return RedirectToAction("ManageDoctor");
        }
        public IActionResult EditDoctor(int DoctorId)
        {
            return View(dc.Doctortbls.Find(DoctorId));
        }
        [HttpPost]
        public IActionResult EditDoctor(Doctortbl obj, List<IFormFile> postedFiles)
        {       
                string wwwpath = env.WebRootPath;
                string contentPath = env.ContentRootPath;

                string path = Path.Combine(env.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();
                foreach (IFormFile postedFile in postedFiles)
                {
                    string filename = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(filename);
                        obj.ProfileImage = filename;
                    }
                }
                //obj.CityId = 1;
                //obj.StateId = 1;
                dc.Doctortbls.Update(obj);
                dc.SaveChanges();
                return RedirectToAction("ManageDoctor");
            }

            //dc.Doctortbls.Update(obj);
            //dc.SaveChanges();
            //return RedirectToAction("ManageDoctor");
        
        public IActionResult DeleteDoctor(int Id)
        {
            return View(dc.Doctortbls.Find(Id));
        }
        [HttpPost]
        [ActionName("DeleteDoctor")]
        public IActionResult DeleteDoctorRec(int Id)
        {
            dc.Doctortbls.Remove(dc.Doctortbls.Find(Id));
            dc.SaveChanges();
            return RedirectToAction("ManageDoctor");
        }

    }
}


