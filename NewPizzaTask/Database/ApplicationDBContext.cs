namespace NewPizzaTask.Database
{
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using NewPizzaTask.Models;
    using NewPizzaTask.SecurityModels;
    using MySql.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
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
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            return new ApplicationDBContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties().Where(x =>
                    x.PropertyType.FullName != null &&
                    (x.PropertyType.FullName.Equals("System.String") &&
                    !x.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(q => q.TypeName != null &&
                    q.TypeName.Equals("varchar(max)", StringComparison.InvariantCultureIgnoreCase)))).Configure(c =>
                    c.HasColumnType("varchar(65000)"));

            modelBuilder.Properties().Where(x =>
                    x.PropertyType.FullName != null &&
                    (x.PropertyType.FullName.Equals("System.String") &&
                    !x.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(q => q.TypeName != null &&
                    q.TypeName.Equals("nvarchar", StringComparison.InvariantCultureIgnoreCase)))).Configure(c =>
                    c.HasColumnType("varchar"));

            //    modelBuilder.Entity<IdentityRole>().HasKey(x => x.Id);
            //    modelBuilder.Entity<IdentityUserLogin>().HasKey(x => x.UserId);
            //   modelBuilder.Entity<IdentityUserRole>().HasKey(x => new { x.RoleId, x.UserId });
        }


        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }



       

    }

}