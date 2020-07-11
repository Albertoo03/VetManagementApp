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
        
        //public void Add(Customer entity)
        //{
        //    using (var context = new VetManagementAppDbContext())
        //    {
        //        var queryResult = context.Customers.Add(entity);
        //        context.SaveChanges();
        //    }
        //}

        //public bool Delete(int customerId)
        //{
        //    using (var context = new VetManagementAppDbContext())
        //    {
        //        var customerToRemove = context.Customers.Where(customer => customer.Id == customerId).FirstOrDefault();

        //        if (customerToRemove == null)
        //            return false;

        //        context.Customers.Remove(customerToRemove);
        //        context.SaveChanges();

        //        return true;
        //    }
        //}

        //public ICollection<Customer> GetAll()
        //{
        //    using(var context = new VetManagementAppDbContext())
        //    {
        //        var queryResult = context.Customers.ToList();

        //        return queryResult;
        //    }
        //}

        //public ObservableCollection<Customer> All
        //{
        //    get
        //    {
        //        _dbSet.Load();
        //        var queryResult = _dbSet.Local;

        //        return queryResult;                
        //    }
        //}


        public ICollection<Animal> GetAllAnimals(Customer customer)
        {

            var queryResult = _vetDbContext.Animals.Where(animal => animal.Owner == customer).ToList();

            return queryResult;
            //using (var context = new VetManagementAppDbContext())
            //{
            //    var queryResult = context.TreatedAnimals.Where(animal => animal.Owner == customer).ToList();

            //    return queryResult;
            //}
        }

        public IEnumerable<Appointment> GetPastAppointments(Customer customer)
        {
            var queryResult = _vetDbContext.Appointments.Where(app => app.AppointedCustomer == customer && app.Date < DateTime.Now.Date).ToList();

            return queryResult;
        }

        public IEnumerable<Animal> GetTreatedAnimals(Customer customer)
        {
            var queryResult = _vetDbContext.Animals.Where(animal => animal.Owner == customer && animal.IsCurrentlyBeingTreated == true).ToList();

            return queryResult;
        }

    }
}
