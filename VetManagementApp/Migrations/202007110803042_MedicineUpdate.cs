namespace VetManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicineUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicines", "Dose", c => c.String());
            DropColumn("dbo.Medicines", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medicines", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Medicines", "Dose");
        }
    }
}
