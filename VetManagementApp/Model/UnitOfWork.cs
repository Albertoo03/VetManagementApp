﻿using System;
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
        private CommonRepository<Appointment> _appointments;
        private CommonRepository<AnimalBasicInfo> _animalBasicInfos;

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

        public ICommonRepository<Appointment> Appointments
        {
            get => _appointments ?? new CommonRepository<Appointment>(_dbContext);
        }
        public ICommonRepository<AnimalBasicInfo> AnimalBasicInfos
        {
            get => _animalBasicInfos ?? new CommonRepository<AnimalBasicInfo>(_dbContext);
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