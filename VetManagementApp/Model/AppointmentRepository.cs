﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using VetManagementApp.Model.DbContexts;
using static VetManagementApp.Interfaces.Interfaces;

namespace VetManagementApp.Model
{
    public class AppointmentRepository : CommonRepository<Appointment>, IAppointmentRepository
    {
        private readonly VetManagementAppDbContext _vetDbContext;

        public AppointmentRepository(VetManagementAppDbContext dbContext) : base(dbContext)
        {
            this._vetDbContext = dbContext;
        }

        public IEnumerable<Appointment> GetPastAppointments(Customer customer)
        {
            var queryResult = _vetDbContext.Appointments.Where(app => app.AppointedCustomer == customer && app.Date < DateTime.Now.Date).ToList();

            return queryResult;
        }

        public override ICollection<Appointment> GetAll()
        {
            return All;
        }

        public override ICollection<Appointment> All
        {
            get
            {
                _vetDbContext.Appointments.Include(appointment => appointment.AppointedCustomer).Include(app => app.AppointedAnimal.AssignedMedicines).Include(app => app.AppointedAnimal.SpeciesInfo.AvailableMedicines).Load();

                var queryResult = _vetDbContext.Appointments.Local;

                return queryResult;
            }
        }
    }
}
