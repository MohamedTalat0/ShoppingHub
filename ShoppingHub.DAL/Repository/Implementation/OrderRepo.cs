using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingHub.DAL.DataBase;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;

namespace ShoppingHub.DAL.Repository.Implementation
{
    public class OrderRepo:IOrderRepo
    {
        shoppingHubDbContext dbContext;
        public OrderRepo(shoppingHubDbContext context)
        {
            dbContext = context;
        }
        public bool create(Order order)
        {
            {
                try
                {
                    var result=dbContext.Orders.Add(order);
                        dbContext.SaveChanges();
                    if (result.Entity.OrderId > 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public bool updateStatus(int id, string status,string userId) 
        {
            try
            {
                var order = dbContext.Orders.Where(i => i.OrderId == id).FirstOrDefault();
                if (order != null) {
                    if (order.UpdateStatus(status, userId))
                    {
                        dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool cancelOrder(int id)
        {
            try
            {
                var order = dbContext.Orders.Where(i => i.OrderId == id).FirstOrDefault();
                if (order != null)
                {
                    if (order.UpdateStatus("Cancelled", "User"))
                    {
                        dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool delete(int id) {//trial
            try
            {
                var order = dbContext.Orders.Where(i => i.OrderId == id).FirstOrDefault();
                if (order != null) { 
                    dbContext.Orders.Remove(order);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Order getById(int id) {
            try
            {
                var order = dbContext.Orders.Where(i => i.OrderId == id).FirstOrDefault();
                return order;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Order> getAllOrders() {
            try
            {
                List<Order> orders = dbContext.Orders.ToList();
                return orders;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Order> GetUserOrders(string userId)
        {
            try
            {
                var result= dbContext.Orders.Where(o => o.UserId == userId).ToList();
                return result;   

            }
            catch (Exception ex)
            {

                throw ex;
            }
                
        }

     
    }
}
