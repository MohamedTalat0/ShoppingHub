using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class Order
    {
        public int OrderId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public string Status { get; private set; }
        public string ShippingAddress { get; private set; }
        public string PaymentMethod { get; private set; }
        //public double TotalPrice { get; private set; }
        //public int TotalItems { get; private set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<OrderItem> orderItems { get; private set; } = new List<OrderItem>();

    }
}
