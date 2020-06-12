using Microsoft.Ajax.Utilities;
using NewPizzaTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewPizzaTask.Database;
using System.Web.Mvc;

namespace NewPizzaTask.Repository
{
    public class ItemRepository
    {
        public List<SelectListItem> GetAllItems()
        {
            using (var dbContext = new PizzaShopContext())
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                List<Item> items = dbContext.Items.ToList();

                selectListItems = items.Select(item => new SelectListItem
                {
                    Text = item.ItemName,
                    Value = item.ItemId.ToString(),
                    Selected = true

                }).ToList();

                return selectListItems;
            }

        }
    }   
}