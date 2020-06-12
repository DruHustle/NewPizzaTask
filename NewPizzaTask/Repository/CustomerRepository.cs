using NewPizzaTask.Database;
using NewPizzaTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewPizzaTask.Repository
{
    public class CustomerRepository
    {
        public List<SelectListItem> GetAllCustomers()
        {
            using (var dbContext = new PizzaShopContext())
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                List<Customer> customers = dbContext.Customers.ToList();

                selectListItems = customers.Select(customer => new SelectListItem
                {
                    Text = customer.CustomerName,
                    Value = customer.CustomerId.ToString(),
                    Selected = true

                }).ToList();

                return selectListItems;
            }

        }


    }
}