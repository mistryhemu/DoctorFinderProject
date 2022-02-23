using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorFinderProject.Controllers
{
    public class CommonController : Controller
    {
        public  enum AppointmentStatus
        {
            Waiting = 0,
            Confirm,
            Cancelled

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
