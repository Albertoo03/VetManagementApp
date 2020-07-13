namespace VetManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCollectionsOfAnimalsToMedicineTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medicines", "AnimalBasicInfo_Species", "dbo.AnimalBasicInfoes");
            DropIndex("dbo.Medicines", new[] { "AnimalBasicInfo_Species" });
            CreateTable(
                "dbo.AnimalBasicInfoMedicines",
                c => new
                    {
                        AnimalBasicInfo_Species = c.String(nullable: false, maxLength: 128),
                        Medicine_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnimalBasicInfo_Species, t.Medicine_Id })
                .ForeignKey("dbo.AnimalBasicInfoes", t => t.AnimalBasicInfo_Species, cascadeDelete: true)
                .ForeignKey("dbo.Medicines", t => t.Medicine_Id, cascadeDelete: true)
                .Index(t => t.AnimalBasicInfo_Species)
                .Index(t => t.Medicine_Id);
            
            DropColumn("dbo.Medicines", "AnimalBasicInfo_Species");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medicines", "AnimalBasicInfo_Species", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AnimalBasicInfoMedicines", "Medicine_Id", "dbo.Medicines");
            DropForeignKey("dbo.AnimalBasicInfoMedicines", "AnimalBasicInfo_Species", "dbo.AnimalBasicInfoes");
            DropIndex("dbo.AnimalBasicInfoMedicines", new[] { "Medicine_Id" });
            DropIndex("dbo.AnimalBasicInfoMedicines", new[] { "AnimalBasicInfo_Species" });
            DropTable("dbo.AnimalBasicInfoMedicines");
            CreateIndex("dbo.Medicines", "AnimalBasicInfo_Species");
            AddForeignKey("dbo.Medicines", "AnimalBasicInfo_Species", "dbo.AnimalBasicInfoes", "Species");
        }
    }
}
