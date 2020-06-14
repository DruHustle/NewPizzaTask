namespace NewPizzaTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        CartStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 100, unicode: false),
                        CategoryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                        ModifiedDate = c.DateTime(nullable: false, precision: 0),
                        ProductImage = c.String(maxLength: 1000, unicode: false),
                        IsFeatured = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PriceUSD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceEUR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cart_CartId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Cart", t => t.Cart_CartId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.Cart_CartId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000, unicode: false),
                        Surname = c.String(maxLength: 1000, unicode: false),
                        PhoneNumber = c.Int(nullable: false),
                        Address = c.String(maxLength: 1000, unicode: false),
                        City = c.String(maxLength: 1000, unicode: false),
                        State = c.String(maxLength: 1000, unicode: false),
                        Country = c.String(maxLength: 1000, unicode: false),
                        PaymentType = c.String(maxLength: 1000, unicode: false),
                        Selected = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128, unicode: false),
                        Cart_CartId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Cart", t => t.Cart_CartId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Cart_CartId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 1000, unicode: false),
                        SecurityStamp = c.String(maxLength: 1000, unicode: false),
                        PhoneNumber = c.String(maxLength: 1000, unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                        ClaimType = c.String(maxLength: 1000, unicode: false),
                        ClaimValue = c.String(maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 128, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                        RoleId = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, unicode: false),
                        Name = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderDetail", "Cart_CartId", "dbo.Cart");
            DropForeignKey("dbo.OrderDetail", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Product", "Cart_CartId", "dbo.Cart");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.OrderDetail", new[] { "Cart_CartId" });
            DropIndex("dbo.OrderDetail", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Product", new[] { "Cart_CartId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
            DropTable("dbo.Cart");
        }
    }
}
