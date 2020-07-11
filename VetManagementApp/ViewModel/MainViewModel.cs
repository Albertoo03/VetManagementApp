using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using VetManagementApp.Model;
using VetManagementApp.Model.DbContexts;

namespace VetManagementApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ICommand _addNewCustomerAsyncCommand;

        public ICommand AddNewCustomerAsyncCommand
        {
            get => _addNewCustomerAsyncCommand ?? new RelayCommand(() => AddNewCustomer());
        }

        private ICommand _removeSelectedCustomerAsyncCommand;
        public ICommand RemoveSelectedCustomerAsyncCommand
        {
            get => _removeSelectedCustomerAsyncCommand ?? new RelayCommand(() => RemoveSelectedCustomer());
        }

        public void AddNewCustomer()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Customer customer = new Customer();
                customer.Id = 1;
                customer.FirstName = "George";
                customer.LastName = "Bake";
                customer.OwnedAnimals = null;
                customer.Appointments = null;
                //customer.Address = "USA";
                customer.Contact = "55555444";
                unitOfWork.Customers.Add(customer);
                //var c = unitOfWork.Customers.All;
                //this.Customers.Add(customer);
                unitOfWork.Save();

                RaisePropertyChanged(() => Customers);
            }
        }

        public void RemoveSelectedCustomer()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (SelectedCustomer == null)
                    return;

                unitOfWork.Customers.Delete(SelectedCustomer.Id);

                unitOfWork.Save();

                RaisePropertyChanged(() => Customers);
            }
        }

        private ObservableCollection<Customer> _customers;
        //public ObservableCollection<Customer> Customers
        //{
        //    get => _customers;
        //    set
        //    {
        //        _customers = value;
        //        RaisePropertyChanged(() => Customers);
        //    }
        //}
        public ObservableCollection<Customer> Customers
        {
            get
            {
                using (var uow = new UnitOfWork())
                {
                    return new ObservableCollection<Customer>(uow.Customers.All);
                }
                
            }
        }
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged(() => SelectedCustomer);
            }
        }
        //{
        //    get
        //    {
        //        return (ObservableCollection<Customer>)customerRepo.All;
        //        //using (var customerRepo = new CustomerRepository())
        //        //{
        //        //    return customerRepo.All;
        //        //}
        //    }
        //}
        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                using (var uow = new UnitOfWork())
                {
                    return new ObservableCollection<Appointment>(uow.Appointments.All);
                }
            }

        }

        public ObservableCollection<Animal> CurrentlyTreatedAnimals { get; set; }
        public ObservableCollection<Animal> AnimalsTreatedInThePast { get; set; }


        public void ShowAnimals()
        {
            using(var context = new VetManagementAppDbContext())
            {
                var animals = context.Animals.ToList();
                //Customers = context.Customers.Local;
            }

        }

        public void ShowCustomers()
        {
            using(var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Customers.Add(new Customer());
                unitOfWork.Customers.Delete(0);

                unitOfWork.Save();
            }
        }

        public void AddCustomer()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Customer customer = new Customer();
                customer.Id = 1;
                customer.FirstName = "Steve";
                customer.LastName = "Jobs";
                customer.OwnedAnimals = null;
                customer.Appointments = null;
                //customer.Address = "USA";
                customer.Contact = "55555444";
                unitOfWork.Customers.Add(customer);
                //unitOfWork.Customers.Delete(0);

                unitOfWork.Save();
            }
        }

        public void AddCustomer2(string name)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Customer customer = new Customer();
                customer.Id = 1;
                customer.FirstName = "Stevenws";
                customer.LastName = name;
                customer.OwnedAnimals = null;
                customer.Appointments = null;
                //customer.Address = "USA";
                customer.Contact = "55555444";
                unitOfWork.Customers.Add(customer);
                //var c = unitOfWork.Customers.All;
                //this.Customers.Add(customer);
                unitOfWork.Save();
            }
        }

        public MainViewModel()
        {
 
        }




    }


}
