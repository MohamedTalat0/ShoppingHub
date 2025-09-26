using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingHub.DAL.Entities;

namespace ShoppingHub.BLL.ModelVm.order
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; } // Pending, Shipped, Delivered, Cancelled
        public List<CartItem> Items { get; set; }
    }
}
