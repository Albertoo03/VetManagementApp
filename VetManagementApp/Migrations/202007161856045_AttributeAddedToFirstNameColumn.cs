namespace VetManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttributeAddedToFirstNameColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "FirstName", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "FirstName", c => c.String());
        }
    }
}
