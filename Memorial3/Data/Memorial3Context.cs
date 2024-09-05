using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Memorial3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Memorial3.Data
{
    public class Memorial3Context : IdentityDbContext<DefaultUser>
    {
        public Memorial3Context (DbContextOptions<Memorial3Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Memorial> Memorial { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Media> Media { get; set; }


    }
}
