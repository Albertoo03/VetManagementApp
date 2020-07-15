using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetManagementApp.Model.DbContexts
{
    public class VetManagementAppDbContext : DbContext
    {
        public VetManagementAppDbContext() : base("VetManagementAppDb")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.ComplexType<Animal>()
        //        .Property(p => p.SpeciesInfo.Species).IsOptional();

        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<AnimalBasicInfo> AnimalsBasicInfos { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Animal> Animals { get; set; }
    }
}
