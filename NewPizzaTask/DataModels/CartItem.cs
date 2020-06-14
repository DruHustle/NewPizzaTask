using System.ComponentModel.DataAnnotations;
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