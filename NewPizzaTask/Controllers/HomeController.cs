using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NewPizzaTask.Database;
using NewPizzaTask.Models;
using NewPizzaTask.DataModels;
using PagedList;

namespace NewPizzaTask.Controllers
{

    public class HomeController : Controller
    {
        readonly ApplicationDBContext dBContext;

        public HomeController()
        {
            dBContext = new ApplicationDBContext();
        }

        public ActionResult Index(string search, int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 8;

            IPagedList<Product> productList = string.IsNullOrWhiteSpace(search)
                ? this.dBContext.Products.Where(i => i.IsDelete == false).OrderBy(x => x.ProductId).ToPagedList(pageIndex, pageSize)
                : this.dBContext.Products.Where(i => i.IsDelete == false).Where(x => (x.ProductName ?? String.Empty).ToLower().Contains(search.ToLower())).OrderBy(x => x.ProductId).ToPagedList(pageIndex, pageSize);

            return View(productList);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                var product = dBContext.Products.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            CartItem cartItem = new CartItem
                            {
                                Product = product,
                                Quantity = prevQty-1
                            };
                            cart.Add(cartItem);
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }

        
        public ActionResult AddToCart(int productId, string url)
        {
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                var product = dBContext.Products.Find(productId);
                CartItem cartItem = new CartItem
                {
                    Product = product,
                    Quantity = 1
                };                
                cart.Add(cartItem);
               
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                var product = dBContext.Products.Find(productId);
                int count = cart.Count();      

                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        CartItem cartItem = new CartItem()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        };
                        cart.Insert(i,cartItem);

                        break;
                    }

                    var prd = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();

                    if (prd == null)
                    {
                        CartItem cartItem = new CartItem
                        {
                            Product = product,
                            Quantity = 1
                        };
                        cart.Add(cartItem);
                    }

                    Session["cart"] = cart;
                }
                
            }
            
         return Redirect(url);
        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);                    
                 
                    break;
                }
            }

            Session["cart"] = cart;
            return Redirect("Index");
        }
    }
}