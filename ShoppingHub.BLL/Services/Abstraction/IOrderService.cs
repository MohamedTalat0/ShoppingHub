using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingHub.BLL.ModelVm.order;
using ShoppingHub.DAL.Entities;

namespace ShoppingHub.BLL.Services.Abstraction
{
    public interface IOrderService
    {
        public (bool, string) create(CreateOrderVM order,string userId);
        public (bool, string?, List<getAllOrdersVM>) getAll();
        public (bool, string?, getAllOrdersVM order) getById(int id);
        public(bool,string?) updatStatus(int id,string status, string userId);
        public (bool, string?) delete(int id);
        public (bool, string?) cancelOrder(int id);
        public List<OrderVM> GetUserOrders(string userId);

    }
}
