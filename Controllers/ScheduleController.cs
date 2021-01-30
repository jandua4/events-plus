using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        //public string Index()
        {
            //return "This is the default returned view in the ScheduleController. \n" +
            //    "This method should return the schedule. Sort Events table by Date DESC.";


            return View();
        }

        public string Help()
        {
            return "This is the default returned view in the ScheduleController. \n" +
            "This method should return the schedule. Sort Events table by Date DESC.";
        }

    }
}
