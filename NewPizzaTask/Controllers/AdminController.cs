using Newtonsoft.Json;
using NewPizzaTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewPizzaTask.Database;
using System.Data.Entity;

namespace NewPizzaTask.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        readonly ApplicationDBContext dBContext = new ApplicationDBContext();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<Category> categories = dBContext.Categories.ToList();

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
            return View(dBContext.Products.ToList());
        }

        public ActionResult ProductEdit(int productId)
        {                      
            return View(dBContext.Products.Find(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
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
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            tbl.ModifiedDate = DateTime.Now;
            dBContext.Products.Add(tbl);
            dBContext.SaveChanges();
            return RedirectToAction("Product");
        }
    }
}