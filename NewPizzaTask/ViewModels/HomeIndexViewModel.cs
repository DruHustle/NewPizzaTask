using System.Linq;
using PagedList;
using NewPizzaTask.Database;
using NewPizzaTask.Models;
using System.Web.Configuration;
using System;

namespace NewPizzaTask.ViewModels
{
    public class HomeIndexViewModel
    {
        //public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        readonly ApplicationDBContext dBContext = new ApplicationDBContext();
        public IPagedList<Product> ListOfProducts { get; set; }

        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            int pageIndex = page ?? 1;

            IPagedList<Product> data = string.IsNullOrWhiteSpace(search)
                ? this.dBContext.Products.OrderBy(x => x.ProductId).ToPagedList(pageIndex, pageSize)
                : this.dBContext.Products.Where(x => (x.ProductName ?? String.Empty).ToLower().Contains(search.ToLower())).OrderBy(x => x.ProductId).ToPagedList(pageIndex, pageSize);

            return new HomeIndexViewModel
            {
                ListOfProducts = data
            };
        }
    }
}