using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

     









        public IActionResult Error()
        {
            return View();
        }
    }
}
