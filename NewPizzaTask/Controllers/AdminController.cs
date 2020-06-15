using NewPizzaTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewPizzaTask.Database;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NewPizzaTask.Controllers
{
    //only authorized admin roles access views that are connected here hence login redirection 
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        readonly ApplicationDBContext dBContext = new ApplicationDBContext();
       // private readonly UserManager<IdentityUser> _userManager;
        public AdminController()
        {
           // _userManager = new UserManager<IdentityUser>();
        }

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<Category> categories = dBContext.Categories.Where(i => i.IsDelete == false).ToList();

            selectListItems = categories.Select(category => new SelectListItem
            {
                Text = category.CategoryName,
                Value = category.CategoryId.ToString(),
                Selected = true

            }).ToList();

            return selectListItems;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<Category> allcategories = dBContext.Categories.Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            Category cd = new Category();

            return View(cd);
            
        }

        [HttpPost]
        public ActionResult AddCategory(Category cd)
        {
            if (ModelState.IsValid)
            {
                dBContext.Categories.Add(cd);
                dBContext.SaveChanges();
                return RedirectToAction("Categories");

            }
            return View(cd);
        }

        public ActionResult CategoryDelete(int catId)
        {
            Category cd = dBContext.Categories.Find(catId);
            cd.IsDelete = true;
            dBContext.Entry(cd).State = EntityState.Modified;
            dBContext.SaveChanges();

            return RedirectToAction("Categories");
        }

        public ActionResult CategoryEdit(int catId)
        {
            return View(dBContext.Categories.Find(catId));
        }

        [HttpPost]
        public ActionResult CategoryEdit(Category tbl)
        {
            dBContext.Entry(tbl).State = EntityState.Modified;
            dBContext.SaveChanges();
            return RedirectToAction("Categories");
        }

        public ActionResult Product()
        {
            return View(dBContext.Products.Where(i => i.IsDelete == false).ToList());
        }
        
        public ActionResult ProductDelete(int productId)
        {
            Product tbl = dBContext.Products.Find(productId);
            tbl.ModifiedDate = DateTime.Now;
            tbl.IsDelete = true;
            dBContext.Entry(tbl).State = EntityState.Modified;
            dBContext.SaveChanges();

            return RedirectToAction("Product");
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(dBContext.Products.Find(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/PizzaImages/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = file != null ? pic : tbl.ProductImage;
            tbl.ModifiedDate = DateTime.Now;

            dBContext.Entry(tbl).State = EntityState.Modified;
            dBContext.SaveChanges();
            return RedirectToAction("Product");
        }

        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/PizzaImages/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            tbl.ModifiedDate = Convert.ToDateTime("05 / 05 / 2005");
            tbl.PriceUSD = tbl.PriceEUR * Convert.ToDecimal(1.1300);
            dBContext.Products.Add(tbl);
            dBContext.SaveChanges();
            return RedirectToAction("Product");
        }
    }
}