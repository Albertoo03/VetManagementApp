using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using VetManagementApp.Model;

namespace VetManagementApp.Interfaces
{
    public class Interfaces
    {
        public interface IAnimal
        {
            ObservableCollection<Medicine> GetListOfAvailableMedicines();
        }

        public interface IMedicine
        {

        }

        public interface ICustomerRepository : ICommonRepository<Customer>
        {
            IEnumerable<Appointment> GetPastAppointments(Customer customer);
            ICollection<Animal> GetAllAnimals(Customer customer);
            IEnumerable<Animal> GetTreatedAnimals(Customer customer);
            
        }

        public interface ICommonRepository<T>
        {
            ICollection<T> GetAll();
            T Get(int entityId);
            void Add(T entity);
            bool Delete(int entityId);
            ObservableCollection<T> All { get; }

        }

        public interface IUnitOfWork
        {
            ICustomerRepository Customers { get; }

            void Save();
        }
    }
}
