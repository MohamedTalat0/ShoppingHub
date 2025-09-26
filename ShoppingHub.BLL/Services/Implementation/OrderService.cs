using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingHub.BLL.ModelVm.order;
using ShoppingHub.BLL.Services.Abstraction;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;

namespace ShoppingHub.BLL.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _repo;
        private readonly IMapper mapper;


        public OrderService(IOrderRepo repo, IMapper mapper)
        {
            _repo = repo;
            this.mapper = mapper;
        }

        public (bool, string) create(CreateOrderVM order,string userId)
        { 
            try
            {
                var totalPrice = order.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);
                var totalItems = order.CartItems.Sum(ci => ci.Quantity);
                var orderitems = order.CartItems.Select(
                    ci => new OrderItem
                    {
                        ProductID = ci.ProductID,
                        Quantity = ci.Quantity,
                    }).ToList();
            
                DateTime deliverydate= DateTime.Now.AddDays(2);
                string address = order.Address.City + "-" + order.Address.Street + "-" + order.Address.Building;
                Order newOrder = new Order(deliverydate, address, order.PaymentMethod,totalPrice,totalItems,userId, orderitems);
  
                
                var result = _repo.create(newOrder);


                return result ? (result, "") : (result, "we have problem");
            }
            catch (Exception ex)
            {
                return (false,ex.Message);
            }
        }

        public (bool, string?) delete(int id)
        {
            try
            {
                var result= _repo.delete(id);
                return (result, result ? "" : "we have problem here");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (bool, string?, List<getAllOrdersVM>) getAll()
        {
            try
            {
                var orders = _repo.getAllOrders();
                var result=mapper.Map<List<getAllOrdersVM>>(orders);

                return (true, null, result);

            }
            catch (Exception ex)
            {

                return (false, ex.Message,null );

            }
        }
        public (bool, string?,getAllOrdersVM order) getById(int id)
        {
            try
            {
                var order = _repo.getById(id);
                var result=mapper.Map<getAllOrdersVM>(order);

                if(result != null)
                {
                    return (true, null,result);
                }
                return (false,"we have problem",null);
            }
            catch (Exception ex)
            {

                return (false,ex.Message,null);

            }
        }

        public (bool, string?) updatStatus(int id, string status, string userId)
        {
            try
            {
                var result = _repo.updateStatus(id, status,userId);

                return (result, result ? "" : "we have problem here");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public (bool, string?) cancelOrder(int id)
        {
            try
            {
                var result = _repo.cancelOrder(id);

                return (result, result ? "" : "we have problem here");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<OrderVM> GetUserOrders(string userId)
        {
            try
            {
                var result=_repo.GetUserOrders(userId).Where(o => o.UserId == userId)
            .Select(o => new OrderVM
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                Address = o.ShippingAddress,
                PaymentMethod = o.PaymentMethod,
                TotalAmount = o.TotalPrice,
                Status = o.Status,
                Items = o.orderItems.Select(i => new CartItem(userId,i.ProductID,i.Quantity)).ToList()}).ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
