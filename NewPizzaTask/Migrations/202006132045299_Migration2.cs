namespace NewPizzaTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "PhoneNumber", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "Address", c => c.String());
            AddColumn("dbo.OrderDetails", "Selected", c => c.Boolean(nullable: false));
            DropColumn("dbo.OrderDetails", "Adress");
            DropColumn("dbo.OrderDetails", "ZipCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "ZipCode", c => c.String());
            AddColumn("dbo.OrderDetails", "Adress", c => c.String());
            DropColumn("dbo.OrderDetails", "Selected");
            DropColumn("dbo.OrderDetails", "Address");
            DropColumn("dbo.OrderDetails", "PhoneNumber");
        }
    }
}
