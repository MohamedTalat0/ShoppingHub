using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class User : IdentityUser
    {
        public string Role { get; private set; }
        public string ImagePath { get; private set; }
        public string Address {  get; private set; }
        public bool isDeleted { get; private set; } = false;
        public string createdOn { get; private set; }
        public string deletedOn { get; private set; }
        public string lastUpdatedOn { get; private set; }
        public List<Order> Orders { get; private set; } = new List<Order>();
        public List<CartItem> cartItems { get; private set; } = new List<CartItem>();
        public List<ProductRating> Ratings { get; private set; } = new List<ProductRating>();

        public User (
            //string userName, 
            //string Email ,
            //string Phone ,
            string Role ,
            string ImagePath, 
            string Address,string createdOn) {
            //this.UserName = userName;
            //this.Email = Email;
            //this.PhoneNumber = Phone;
            this.Role = Role;
            this.ImagePath = ImagePath;
            this.Address = Address;
            this.isDeleted = false;
            this.deletedOn = "NotDeleted";
            this.lastUpdatedOn = "Not Updated";
            this.createdOn=createdOn.ToString();
        }

        public User()
        {
        }

        public bool update(User user)
        {
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.Role = user.Role;
            this.ImagePath = user.ImagePath;
            this.Address = user.Address;
            this.isDeleted = user.isDeleted;
            return true;
        }
        public bool delete() { 
            return this.isDeleted=true;
        }
        public bool restore()
        {
            return this.isDeleted=false;
        }

    }
}
