using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShoppingHub.DAL.DataBase;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        public readonly shoppingHubDbContext _shoppingHubDbContext;
        public ProductRepo(shoppingHubDbContext _context)
        {
            _shoppingHubDbContext = _context;
        }
        //changed style

        public (bool, string?) AddProduct(Product product)
        {
            try
            {
                _shoppingHubDbContext.Products.Add(product);
                _shoppingHubDbContext.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {

                return (true, ex.Message);
            }
        }


        //public (bool, string?) AddProduct(Product product)
        //{
        //    try
        //    {
        //        _shoppingHubDbContext.Products.Add(product);
        //        _shoppingHubDbContext.SaveChanges();
        //        return (false, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.InnerException?.Message);
        //        return (true, ex.Message);
        //    }
        //}

        //public void EditProduct(Product product)
        //{
        //    _shoppingHubDbContext.Products.Update(product);
        //    _shoppingHubDbContext.SaveChanges();
        //}

        

        public List<Product> GetAllProducts()
        {
            try
            {
                var result = _shoppingHubDbContext.Products
                                               .Where(p => !p.ISRemoved)
                                               .ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching products.", ex);
            }
        }

        public Product GetProductByID(int id)
        {
            try
            {

                var product = _shoppingHubDbContext.Products
                                                   .FirstOrDefault(p => p.ProductId == id && !p.ISRemoved);

                if (product == null)
                {
                    throw new ApplicationException($"Product with ID {id} not found or already removed.");
                }

                return product;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An error occurred while fetching the product details.", ex);
            }
        }

        public (bool, string?) Remove(int id)
        {
            try
            {
                var product = _shoppingHubDbContext.Products.FirstOrDefault(p => p.ProductId == id);

                if (product == null || product.ISRemoved)
                {
                    return (false, "THE PRODUCT IS NONEXISTENT OR ALREADY DELETED");
                }

                product.Remove();
                _shoppingHubDbContext.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }

        public Product GetProductByIdEditversion(int id)
        {
          var product= _shoppingHubDbContext.Products
                .Include(p => p.Category) // optional if you need category
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                throw new ApplicationException($"Product with ID {id} not found or already removed.");
            }

            return product;
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                _shoppingHubDbContext.Products.Update(product);
                _shoppingHubDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool updateQuantity(int productId,int quantity)
        {
            try
            {
                var result = _shoppingHubDbContext.Products.FirstOrDefault(i => i.ProductId == productId).updateQuantity(quantity);
                _shoppingHubDbContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Product? GetProductWithRatings(int productId)
        {
            return _shoppingHubDbContext.Products
                           .Include(p => p.Ratings)
                            .ThenInclude(r => r.User)
                           .FirstOrDefault(p => p.ProductId == productId);
        }
    }
}

