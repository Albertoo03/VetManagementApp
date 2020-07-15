using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using VetManagementApp.Model.DbContexts;
using static VetManagementApp.Interfaces.Interfaces;

namespace VetManagementApp.Model
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private VetManagementAppDbContext _dbContext;
        private CustomerRepository _customers;
        private AppointmentRepository _appointments;
        private AnimalBasicInfoRepository _animalBasicInfos;
        private CommonRepository<Medicine> _medicines;
        private AnimalRepository _animals;


        private bool _disposed = false; 

        public UnitOfWork()
        {
            this._dbContext = new VetManagementAppDbContext();
        }

        public UnitOfWork(VetManagementAppDbContext dbContext)
        {
            this._dbContext = dbContext;
            //Customers = new CustomerRepository(_dbContext);
        }

        public ICustomerRepository Customers
        {
            get => _customers ?? new CustomerRepository(_dbContext);
        }

        public IAppointmentRepository Appointments
        {
            get => _appointments ?? new AppointmentRepository(_dbContext);
        }
        public IAnimalBasicInfoRepository AnimalBasicInfos
        {
            get => _animalBasicInfos ?? new AnimalBasicInfoRepository(_dbContext);
        }
        public ICommonRepository<Medicine> Medicines
        {
            get => _medicines ?? new CommonRepository<Medicine>(_dbContext);
        }
        public IAnimalRepository Animals
        {
            get => _animals ?? new AnimalRepository(_dbContext);
        }

        protected virtual void Dispose(bool haveToDispose)
        {
            if (!this._disposed)
            {
                if(haveToDispose)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
