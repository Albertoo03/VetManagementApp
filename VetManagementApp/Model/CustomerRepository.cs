using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetManagementApp.Model.DbContexts;
using static VetManagementApp.Interfaces.Interfaces;

namespace VetManagementApp.Model
{
    public class CustomerRepository : CommonRepository<Customer>, ICustomerRepository
    {
        private readonly VetManagementAppDbContext _vetDbContext;

        public CustomerRepository(VetManagementAppDbContext dbContext) : base(dbContext)
        {
            this._vetDbContext = dbContext;
        }
 

        public ICollection<Animal> GetAllAnimals(Customer customer)
        {

            var queryResult = _vetDbContext.Animals.Where(animal => animal.Owner == customer).ToList();

            return queryResult;
        }



        public IEnumerable<Animal> GetTreatedAnimals(Customer customer)
        {
            var queryResult = _vetDbContext.Animals.Where(animal => animal.Owner == customer && animal.IsCurrentlyBeingTreated == true).ToList();

            return queryResult;
        }

        public ICollection<Appointment> GetAppointments(Customer customer)
        {

            var listOfAppointments = _vetDbContext.Appointments.Where(cust => cust.AppointedCustomer.Id == customer.Id).ToList();
            
            return listOfAppointments;
        }


        public override ICollection<Customer> All
        {
            get
            {
                _vetDbContext.Customers.Include(p => p.Appointments).Include(p => p.OwnedAnimals.Select(animal => animal.SpeciesInfo)).Load();

                var queryResult = _vetDbContext.Customers.Local;

                return queryResult;
            }
        }

    }
}
