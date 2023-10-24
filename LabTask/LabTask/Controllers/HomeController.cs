using LabTask.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Product_catagories.Models;
using Product_catagories.Auth;


namespace LabTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            var db = new ShopEntities();
            ViewBag.Catagory = db.Catagories.ToList();

            var products = db.Products.ToList();
            return View(products);
        }
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            var db = new ShopEntities();
            db.Products.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult EditCatagory(int Id)
        {
            var db = new ShopEntities();
            var data = db.Catagories.Find(Id);
            return View(data);
        }
        [HttpPost]
        public ActionResult EditCatagory(Catagory d)
        {
            var db = new ShopEntities();
            var ex = db.Catagories.Find(d.Id);
            ex.Name = d.Name;
            db.SaveChanges();
            return RedirectToAction("Product");
        }

        [HttpGet]
        public ActionResult EditProduct(int Id)
        {
            var db = new ShopEntities();
            var data = db.Products.Find(Id);
            ViewBag.Catagory = db.Catagories.ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult EditProduct(Product d)
        {
            var db = new ShopEntities();
            var ex = db.Products.Find(d.Id);
            ex.Name = d.Name;
            ex.Price = d.Price;
            ex.Catagory = d.Catagory;
            db.SaveChanges();
            return RedirectToAction("Product", "Home");
        }
        [HttpGet]
        public ActionResult Product()
        {
            var db = new ShopEntities();
            var data = db.Products.ToList();
            ViewBag.Catagory = db.Catagories.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Catagory()
        {
            var db = new ShopEntities();
            var data = db.Catagories.ToList();
            return View(data);
        }


        [HttpGet]
        public ActionResult AddCatagory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCatagory(Catagory p)
        {
            var db = new ShopEntities();
            db.Catagories.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult deleteCatagory(int Id)
        {
            var db = new ShopEntities();
            var data = db.Catagories.Find(Id);
            db.Catagories.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Product", "Home");

        }

        [HttpGet]
        public ActionResult deleteProduct(int Id)
        {
            var db = new ShopEntities();
            var data = db.Products.Find(Id);
            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Product", "Home");
        }
        [Logged]
        [HttpGet]
        public ActionResult ProductList()
        {
            var db = new ShopEntities();
            var products = db.Products.ToList();
            ViewBag.Catagory = db.Catagories.ToList();

            return View(products);
        }
        [Logged]

        [HttpPost]
        public ActionResult ProductList(FormCollection Form)
        {
            var db = new ShopEntities();
            ViewBag.Catagory = db.Catagories.ToList();
            ViewBag.Product = db.Products.ToList();
            Session["SelectedProductIds"] = "";

            string[] selectedProductIds = Form.GetValues("selectedProducts");

            if (selectedProductIds != null)
            {
                // Store the selected product IDs in the session
                Session["SelectedProductIds"] = selectedProductIds;
            }

            return RedirectToAction("Cart");
        }


        [Logged]
        [HttpGet]

        public ActionResult Cart()
        {
            string[] selectedProductIds = Session["SelectedProductIds"] as string[];

            if (selectedProductIds != null)
            {
                var db = new ShopEntities();
                var selectedProductIdsInt = selectedProductIds.Select(id => int.Parse(id)).ToArray();
                var selectedProducts = db.Products.Where(p => selectedProductIdsInt.Contains(p.Id)).ToList();
                ViewBag.SelectedProducts = selectedProducts;
                ViewBag.Catagory = db.Catagories.ToList();

            }

            return View();
        }


        [Logged]
        [HttpPost]
        public ActionResult Cart(FormCollection Form)
        {
            var db = new ShopEntities();
            ViewBag.Catagory = db.Catagories.ToList();
            ViewBag.Product = db.Products.ToList();
            var orders = db.Orders.ToList();
            Order o = new Order();
            o.Customer_Name = Session["Name"].ToString();
            db.Orders.Add(o);
            db.SaveChanges();
            ProductOrder po = new ProductOrder();
            string[] selectedProductIds = Session["SelectedProductIds"] as string[];
            foreach (var item in selectedProductIds)
            {
                po.O_Id = o.Id;
                po.P_Id = int.Parse(item);
                db.ProductOrders.Add(po);
                db.SaveChanges();

            }
            return View(orders);
        }


    }
}