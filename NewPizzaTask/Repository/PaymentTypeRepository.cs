using NewPizzaTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewPizzaTask.Database;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI.WebControls;

namespace NewPizzaTask.Repository
{
    public class PaymentTypeRepository
    {
        public List<SelectListItem> GetAllPaymentType()
        {
            using (var dbContext = new PizzaShopContext())
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                List<PaymentType> paymentTypes = dbContext.PaymentTypes.ToList();

                selectListItems = paymentTypes.Select(type => new SelectListItem
                {
                    Text = type.PaymentTypeName,
                    Value = type.PaymentTypeId.ToString(),
                    Selected = true

                }).ToList();

                return selectListItems;
            }
            
        }
    }
}