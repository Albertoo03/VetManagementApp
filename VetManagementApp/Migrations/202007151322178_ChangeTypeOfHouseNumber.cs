namespace VetManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfHouseNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "HouseNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "HouseNumber");
        }
    }
}
