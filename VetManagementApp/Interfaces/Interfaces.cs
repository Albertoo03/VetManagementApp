using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using VetManagementApp.Model;

namespace VetManagementApp.Interfaces
{
    public class Interfaces
    {
        public interface IAnimalRepository : ICommonRepository<Animal>
        {
            ICollection<Appointment> GetAppointments(Animal animal);
        }

        public interface IAppointmentRepository : ICommonRepository<Appointment>
        {
            IEnumerable<Appointment> GetPastAppointments(Customer customer);
        }

        public interface ICustomerRepository : ICommonRepository<Customer>
        {

            ICollection<Animal> GetAllAnimals(Customer customer);
            IEnumerable<Animal> GetTreatedAnimals(Customer customer);

            ICollection<Appointment> GetAppointments(Customer customer);
            
        }

        public interface IAnimalBasicInfoRepository : ICommonRepository<AnimalBasicInfo>
        {
            void Delete(string species);
            AnimalBasicInfo GetBySpecies(string species);
        }

        public interface ICommonRepository<T>
        {
            ICollection<T> GetAll();
            T Get(int entityId);
            void Add(T entity);
            bool Delete(int entityId);
            ICollection<T> All { get; }
            bool DeleteAll();
            void Delete(Expression<Func<T, bool>> predicate);
            void SetAsUnchanged(T entity);
            void SetAsAdded(T entity);
            T GetByPredicate(Expression<Func<T, bool>> predicate);

            ICollection<T> GetMultipleByPredicate(Expression<Func<T, bool>> predicate);
        }

        public interface IUnitOfWork
        {
            ICustomerRepository Customers { get; }
            IAnimalBasicInfoRepository AnimalBasicInfos { get; }
            IAnimalRepository Animals { get; }
            ICommonRepository<Medicine> Medicines { get; }
            IAppointmentRepository Appointments { get; }

            void Save();
        }
    }
}
