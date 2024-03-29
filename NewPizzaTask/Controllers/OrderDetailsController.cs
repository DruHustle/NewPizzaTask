﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NewPizzaTask.Database;
using NewPizzaTask.Models;

namespace NewPizzaTask.Controllers
{
    public class OrderDetailsController : Controller
    {
        readonly ApplicationDBContext dBContext;

        public OrderDetailsController()
        {
            dBContext = new ApplicationDBContext();
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = dBContext.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }


        public ActionResult Index()
        {            
            List<OrderDetail> orderDetails = dBContext.OrderDetails.ToList();
            return View(orderDetails);                     

        }

        // GET: OrderDetails/Create
        public ActionResult AddDetails()
        {
          return View();         
            
        }

        // POST: OrderDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDetails( OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                dBContext.OrderDetails.Add(orderDetail);
                dBContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult EditDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = dBContext.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetails(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                dBContext.Entry(orderDetail).State = EntityState.Modified;
                dBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult DeleteDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = dBContext.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost]
        [ActionName("DeleteDetails")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = dBContext.OrderDetails.Find(id);
            dBContext.OrderDetails.Remove(orderDetail);
            dBContext.SaveChanges();
            return RedirectToAction("Index");
        }
 

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dBContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
