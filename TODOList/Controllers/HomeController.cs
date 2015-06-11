using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TODOList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //TaskController viewModel = new TaskController();
            //return RedirectToAction("Index", viewModel);
            //return RedirectToAction("Index", "Task");
            return View();
        }
    }
}
