using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm.Cart
{
    public class ViewCartVM
    {
        public List<ViewCartItemVM> CartItems { get; set; }
        public int TotalItems { get;set; }
        public double TotalPrice { get;set; }
    }
}
