using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm.RatingVM
{
   public class ProductRatingVM
    {
        public int ProductID { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public string UserName { get; set; }
    }
}
