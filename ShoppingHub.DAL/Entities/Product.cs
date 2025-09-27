using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductNameAR { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public string Description { get; private set; }
        public string? ImagePath { get; private set; }
        public double? AvgRate { get; private set; } = 0;
        public string AddedOn { get; private set; }
        public string ModifiedOn { get; private set; }
        public bool ISRemoved { get; private set; } = false;
        public string DescriptionAR { get; private set; }

        

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<CartItem> cartItems { get; private set; } = new List<CartItem>();
        public List<OrderItem> orderItems { get; private set; } = new List<OrderItem>();
        public List<ProductRating>? Ratings { get; private set; } = new List<ProductRating>();
<<<<<<< HEAD
=======

>>>>>>> 4ea336c7eaf79dcd14e18f5816e4732f8b4d1e73


        public bool updateQuantity(int quantity)
        {
            if (quantity <= 0 || quantity > Quantity)
            {
                return false;
            }
            Quantity-=quantity;
            return true;
        }

        //public bool Update(Product updated)
        //{
        //    this.ProductName = updated.ProductName;
        //    this.Price = updated.Price;
        //    this.Quantity = updated.Quantity;
        //    this.Description = updated.Description;
        //    this.Category = updated.Category;
        //    this.ISRemoved = updated.ISRemoved;
        //    this.Ratings = updated.Ratings;
        //        this.ImagePath = updated.ImagePath;
        //    this.CATID = updated.CATID;
        //    this.ModifiedOn = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        //    return true;
        //}
        public Product() { }
        public bool Remove()
        {
            return this.ISRemoved = true;
        }

        public Product(
            int productid,
        string ProductName,
        string productnamear,
            string? descriptionar,
        double Price,
        int Quantity,
        string? Description,
        string ImagePath,
        int? categoryid
            
        )
        {
            this.ProductId = productid;
            this.ProductName = ProductName;
            this.ProductNameAR = productnamear;
            this.DescriptionAR = descriptionar;

            this.Price = Price;
            this.Description = Description;
            this.Quantity = Quantity;
            this.ImagePath = ImagePath;

            this.ISRemoved = false;
            this.ModifiedOn = "Yet to be modified";
            AddedOn = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            this.CategoryId = categoryid;
            
        }

        //public bool Update
        //    (string productName,
        //     string productnamear,
        //    string? descriptionar,
        //    double price,
        //    int quantity,
        //    string? description,
        //    string? imagePath,
        //  int? id)
        //{
        //    this.ProductName = productName;
        //    this.ProductNameAR = productnamear;
        //    this.DescriptionAR = descriptionar;

        //    this.Price = price;
        //    this.Quantity = quantity;
        //    this.Description = description;
        //    this.ImagePath = imagePath;
        //    this.CategoryId = id;
            
        //    this.ModifiedOn = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        //    return true;
        //}

        public void UpdatePrice(double price) => Price = price;
        public void UpdateQuantity(int qty) => Quantity = qty;
        public void UpdateDescription(string desc, string descAr)
        {
            Description = desc;
            DescriptionAR = descAr;
        }
        public void UpdateNames(string name, string nameAr)
        {
            ProductName = name;
            ProductNameAR = nameAr;
        }
        public void UpdateImage(string imagePath) => ImagePath = imagePath;

        public void AddOrUpdateRating(string userId, int rate, string? comment = null)
        {
            var existing = Ratings.FirstOrDefault(r => r.UserID == userId);
            if (existing != null)
                existing.UpdateRating(rate, comment);
            else
                Ratings.Add(new ProductRating(userId, ProductId, rate, comment));

            AvgRate = Ratings.Any() ? Ratings.Average(r => r.Rate) : 0;
        }
    }
}

