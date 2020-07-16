namespace VetManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TargetAnimalAddedToMedicineTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicines", "TargetAnimal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicines", "TargetAnimal");
        }
    }
}
