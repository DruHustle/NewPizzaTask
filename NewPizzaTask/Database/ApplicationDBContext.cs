namespace NewPizzaTask.Database
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using NewPizzaTask.Models;
    using NewPizzaTask.SecurityModels;

    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'ApplicationContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'NewPizzaTask.Database.ApplicationContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ApplicationContext' 
        // connection string in the application configuration file.
        public ApplicationDBContext(): base("ApplicationDBConnection")
        {
        }
        public static ApplicationDBContext Create()
        {
            return new ApplicationDBContext();
        }

        
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartStatus> CartStatuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }
        public DbSet<SlideImage> SlideImages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserLogin>().HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserRole>().HasKey(x => new { x.RoleId, x.UserId });
        }

    }

}