using ShoppingHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShoppingHub.BLL.ModelVm
{
  public  class GetAllProductsVM
    {
       
        public string SearchTerm { get; set; }
        public int? SelectedCategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        int productid { get; set; }

       
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

      
        public IEnumerable<OutDisplayingProduct> Products { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }



 public   class OutDisplayingProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAR { get; set; }

        public double Price { get; set; }

        public string ImagePath { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        

    }
}
