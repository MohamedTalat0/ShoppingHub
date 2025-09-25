using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm.Cart
{
    public class ViewCartItemVM
    {
        public string Name { get; set; }
        public string NameAR { get; set; }
        public int Quantity { get; set; }
        public double PricePerItem { get; set; }
        public double TotalPrice { get; set; }
        public string ImagePath { get; set; }
        public int MaxQuantity { get; set; }
        public int ProductID { get; set; }

    }
}
