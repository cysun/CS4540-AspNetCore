using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreExamples.Controllers
{
    public class WikiController : Controller
    {
        public IActionResult Index(string path)
        {
            ViewData["path"] = path;
            return View();
        }
    }
}
