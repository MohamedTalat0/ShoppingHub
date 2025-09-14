using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShoppingHub.DAL.DataBase
{
    internal class shoppingHubDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ShoppingHubDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
