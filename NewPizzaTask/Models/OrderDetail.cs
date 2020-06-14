using System.ComponentModel.DataAnnotations;
using NewPizzaTask.SecurityModels;

namespace NewPizzaTask.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PaymentType { get; set; }
        public bool Selected { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Cart Cart { get; set; }
}
}