using System.ComponentModel.DataAnnotations;

namespace NewPizzaTask.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int CartStatusId { get; set; }

    }
}