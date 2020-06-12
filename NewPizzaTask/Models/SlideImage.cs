using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewPizzaTask.Models
{
    public class SlideImage
    {
        [Key]
        public int SlideId { get; set; }
        public string SlideTitle { get; set; }
        public string SlideImageName { get; set; }
    }
}