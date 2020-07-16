using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetManagementApp.Model.DbContexts;
using static VetManagementApp.Interfaces.Interfaces;

namespace VetManagementApp.Model
{
    public class AnimalRepository : CommonRepository<Animal>, IAnimalRepository
    {
        private readonly VetManagementAppDbContext _vetDbContext;

        public AnimalRepository(VetManagementAppDbContext dbContext) : base(dbContext)
        {
            this._vetDbContext = dbContext;
        }

        public ICollection<Appointment> GetAppointments(Animal animal)
        {

            var listOfAppointments = _vetDbContext.Appointments.Where(cust => cust.AppointedAnimal.Id == animal.Id).ToList();

            return listOfAppointments;
        }

        public override ICollection<Animal> All
        {
            get
            {
                _vetDbContext.Animals.Include(p => p.Appointments).Include(p => p.AssignedMedicines).Include(p => p.Owner).Include(p => p.SpeciesInfo).Include(p => p.SpeciesInfo.AvailableMedicines).Load();

                var queryResult = _vetDbContext.Animals.Local;

                return queryResult;
            }
        }
    }
}
