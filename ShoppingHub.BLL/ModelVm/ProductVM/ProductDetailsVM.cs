using ShoppingHub.BLL.ModelVm.RatingVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm
{
   public class ProductDetailsVM
    {
   
public int id { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAR { get;  set; }
        public string? DescriptionAR { get;  set; }
        public string? Description { get; set; }
        public double Price { get; set; }

        public string ImagePath { get; set; }

        public string CategoryName { get; set; }

        public double? AverageRating { get; set; }

        public List<PRatingVM>? Ratings { get; set; } = new();
    }
}
