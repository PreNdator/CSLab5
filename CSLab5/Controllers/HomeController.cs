using Data;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Data.Common;

namespace CSLab5.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}

