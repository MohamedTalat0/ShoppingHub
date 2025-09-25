using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingHub.DAL.Entities;

namespace ShoppingHub.DAL.Repository.Abstraction
{
    public interface IOrderRepo
    {
        bool create(Order order);
        bool updateStatus(int id,string status,string userId);
        bool cancelOrder(int id);

        bool delete(int id);
        Order getById(int id);
        List<Order> getAllOrders();
    }
}
