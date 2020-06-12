using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using NewPizzaTask.Models;

namespace NewPizzaTask.DataModels
{
    public class CartItem
    {    
        [Key]
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}