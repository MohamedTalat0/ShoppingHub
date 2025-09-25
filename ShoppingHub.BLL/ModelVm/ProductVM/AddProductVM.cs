using Microsoft.AspNetCore.Http;
using ShoppingHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ShoppingHub.BLL.ModelVm
{

    public class AddProductVM
    {
        [Required(ErrorMessage = "Required Field")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public string ProductNameAR { get; set; }
        public string DescriptionAR { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Invalid price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }
        public string Description { get; set; }
       
        public IFormFile ImageFile { get; set; }
        
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public List<Category>? Categories { get; set; }

        public int ID { get; set; }
    }
}
