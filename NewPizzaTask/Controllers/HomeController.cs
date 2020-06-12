using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NewPizzaTask.Database;
using NewPizzaTask.Models;
using NewPizzaTask.ViewModels;

namespace NewPizzaTask.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDBContext dBContext = new ApplicationDBContext();
       
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
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
                var count = cart.Count();
                var product = dBContext.Products.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;

                        cart.Remove(cart[i]);
                        CartItem cartItem = new CartItem
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        };
                        cart.Add(cartItem);
                        
                        break;
                    }
                    else
                    {
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
                    }
                }
                Session["cart"] = cart;
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
                    
                 ;
                    break;
                }
            }

            Session["cart"] = cart;
            return Redirect("Index");
        }
    }
}