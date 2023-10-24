using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabTask.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Homepage()
        {
            return View();
        }
     

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Product_catagories.Models.Login p)
        {
            Session["Name"] = p.Name;

            return RedirectToAction("ProductList", "Home");

        }
    }
}