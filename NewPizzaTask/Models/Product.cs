using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewPizzaTask.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 50)]
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ProductImage { get; set; }
        public bool IsFeatured { get; set; }        
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public int Quantity { get; set; }
        [Required]
        [Range(typeof(decimal), "1", "200000", ErrorMessage = "invalid Price")]
        public decimal PriceUSD { get; set; }
        public decimal PriceEUR { get; set; }

        public virtual Category Category { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
