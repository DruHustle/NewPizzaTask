using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NewPizzaTask.SecurityModels;

namespace NewPizzaTask.Models
{
    public class ShippingDetail
    {
        [Key]
        public int ShippingDetailId { get; set; }
        public int MemberId { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int OrderId { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentType { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}