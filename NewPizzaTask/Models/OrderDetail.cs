using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NewPizzaTask.SecurityModels;

namespace NewPizzaTask.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PaymentType { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Cart Cart { get; set; }
}
}