using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class User
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public string ImagePath { get; private set; }
        public List<Order> Orders { get; private set; } = new List<Order>();
        public List<CartItem> cartItems { get; private set; } = new List<CartItem>();

    }
}
