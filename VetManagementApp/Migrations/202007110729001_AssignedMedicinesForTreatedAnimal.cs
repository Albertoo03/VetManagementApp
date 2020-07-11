namespace VetManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignedMedicinesForTreatedAnimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicines", "Animal_Id", c => c.Int());
            CreateIndex("dbo.Medicines", "Animal_Id");
            AddForeignKey("dbo.Medicines", "Animal_Id", "dbo.Animals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicines", "Animal_Id", "dbo.Animals");
            DropIndex("dbo.Medicines", new[] { "Animal_Id" });
            DropColumn("dbo.Medicines", "Animal_Id");
        }
    }
}
