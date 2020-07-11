namespace VetManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        IsCurrentlyBeingTreated = c.Boolean(nullable: false),
                        Owner_Id = c.Int(),
                        SpeciesInfo_Species = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Owner_Id)
                .ForeignKey("dbo.AnimalBasicInfoes", t => t.SpeciesInfo_Species)
                .Index(t => t.Owner_Id)
                .Index(t => t.SpeciesInfo_Species);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        PurposeOfVisit = c.Int(nullable: false),
                        AppointedAnimal_Id = c.Int(),
                        AppointedCustomer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.AppointedAnimal_Id)
                .ForeignKey("dbo.Customers", t => t.AppointedCustomer_Id)
                .Index(t => t.AppointedAnimal_Id)
                .Index(t => t.AppointedCustomer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnimalBasicInfoes",
                c => new
                    {
                        Species = c.String(nullable: false, maxLength: 128),
                        Group = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Species);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Manufacturer = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AnimalBasicInfo_Species = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnimalBasicInfoes", t => t.AnimalBasicInfo_Species)
                .Index(t => t.AnimalBasicInfo_Species);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "SpeciesInfo_Species", "dbo.AnimalBasicInfoes");
            DropForeignKey("dbo.Medicines", "AnimalBasicInfo_Species", "dbo.AnimalBasicInfoes");
            DropForeignKey("dbo.Animals", "Owner_Id", "dbo.Customers");
            DropForeignKey("dbo.Appointments", "AppointedCustomer_Id", "dbo.Customers");
            DropForeignKey("dbo.Appointments", "AppointedAnimal_Id", "dbo.Animals");
            DropIndex("dbo.Medicines", new[] { "AnimalBasicInfo_Species" });
            DropIndex("dbo.Appointments", new[] { "AppointedCustomer_Id" });
            DropIndex("dbo.Appointments", new[] { "AppointedAnimal_Id" });
            DropIndex("dbo.Animals", new[] { "SpeciesInfo_Species" });
            DropIndex("dbo.Animals", new[] { "Owner_Id" });
            DropTable("dbo.Medicines");
            DropTable("dbo.AnimalBasicInfoes");
            DropTable("dbo.Customers");
            DropTable("dbo.Appointments");
            DropTable("dbo.Animals");
        }
    }
}
