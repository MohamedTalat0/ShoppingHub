using Microsoft.AspNetCore.Http;
using ShoppingHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm
{
  public  class EditProductVM
    {

          
            public string ProductName { get; set; }
        public string ProductNameAR { get;  set; }
        public string? DescriptionAR { get; set; }
        public int ProductID { get; set; }
        
        public double Price { get; set; }             
            public int Quantity { get; set; }              
            public string? Description { get; set; }       
            public string ExistingImagePath { get; set; }  
            public IFormFile? NewImageFile { get; set; }   

        public int? CategoryId { get; set; }
        public List<Category>? Categories { get; set; } 
    }
}
