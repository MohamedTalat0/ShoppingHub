using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm.Cart
{
    public class ViewCartVM
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public int MaxQuantity { get; set; }
        public int ProductID { get; set; }

    }
}
