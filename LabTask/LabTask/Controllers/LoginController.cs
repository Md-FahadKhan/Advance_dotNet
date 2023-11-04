using LabTask.EF;
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

        //[HttpPost]
        //public ActionResult Login(Product_catagories.Models.Login p)
        //{
        //    Session["Name"] = p.Name;
        //    var db = new ShopEntities();


        //    return RedirectToAction("ProductList", "Home");

        //}

        [HttpPost]
        public ActionResult Login(Product_catagories.Models.Login p)
        {
            var db = new ShopEntities(); // Make sure you have an instance of your data context (ShopEntities).
            var user = db.Users.FirstOrDefault(u => u.Username == p.Name && u.Password == p.Password);

            if (user != null)
            {
                Session["Name"] = p.Name;
                // 
                Session["UserType"] = user.Usertype; // Assuming "UserType" is a field in your User model.

                if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
                {
      
                    return RedirectToAction("Product", "Home");
                }
                else if (Session["UserType"] != null && Session["UserType"].ToString() == "Customer")
                {
                    return RedirectToAction("ProductList", "Home");
                }
            }

            // Handle login failure here, e.g., return to the login page with an error message.
            return View("Login");
        }




    }
}