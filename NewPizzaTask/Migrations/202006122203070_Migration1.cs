namespace NewPizzaTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ShippingDetails", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropIndex("dbo.ShippingDetails", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Adress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                        PaymentType = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Cart_CartId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Cart_CartId);
            
            AddColumn("dbo.Products", "PriceUSD", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "PriceEUR", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Cart_CartId", c => c.Int());
            CreateIndex("dbo.Products", "Cart_CartId");
            AddForeignKey("dbo.Products", "Cart_CartId", "dbo.Carts", "CartId");
            DropColumn("dbo.Carts", "ProductId");
            DropColumn("dbo.Carts", "MemberId");
            DropColumn("dbo.Products", "Price");
            DropTable("dbo.CartStatus");
            DropTable("dbo.ShippingDetails");
            DropTable("dbo.SlideImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SlideImages",
                c => new
                    {
                        SlideId = c.Int(nullable: false, identity: true),
                        SlideTitle = c.String(),
                        SlideImageName = c.String(),
                    })
                .PrimaryKey(t => t.SlideId);
            
            CreateTable(
                "dbo.ShippingDetails",
                c => new
                    {
                        ShippingDetailId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Adress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                        OrderId = c.Int(nullable: false),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ShippingDetailId);
            
            CreateTable(
                "dbo.CartStatus",
                c => new
                    {
                        CartStatusId = c.Int(nullable: false, identity: true),
                        CartStatusName = c.String(),
                    })
                .PrimaryKey(t => t.CartStatusId);
            
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Carts", "MemberId", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "Cart_CartId", "dbo.Carts");
            DropForeignKey("dbo.OrderDetails", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Products", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.OrderDetails", new[] { "Cart_CartId" });
            DropIndex("dbo.OrderDetails", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Products", new[] { "Cart_CartId" });
            DropColumn("dbo.Products", "Cart_CartId");
            DropColumn("dbo.Products", "PriceEUR");
            DropColumn("dbo.Products", "PriceUSD");
            DropTable("dbo.OrderDetails");
            CreateIndex("dbo.ShippingDetails", "ApplicationUser_Id");
            CreateIndex("dbo.Carts", "ProductId");
            AddForeignKey("dbo.ShippingDetails", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
