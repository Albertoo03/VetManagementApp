namespace VetManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOnCustomerClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "Street", c => c.String());
            AddColumn("dbo.Customers", "PostalCode", c => c.String());
            DropColumn("dbo.Customers", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Address", c => c.String());
            DropColumn("dbo.Customers", "PostalCode");
            DropColumn("dbo.Customers", "Street");
            DropColumn("dbo.Customers", "City");
        }
    }
}
