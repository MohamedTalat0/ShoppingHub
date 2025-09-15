using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<Product> Products { get; private set; } = new List<Product>();
    }
}
