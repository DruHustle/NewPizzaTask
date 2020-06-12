using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewPizzaTask.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int CartStatusId { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}