using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using VetManagementApp.Helpers;
using VetManagementApp.Model;
using VetManagementApp.Model.DbContexts;
using VetManagementApp.View;
using static VetManagementApp.Helpers.HelpfulUtilities;

namespace VetManagementApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Private variables
        private string _animalBasicInfoToAddSpecies;
        private AnimalGroup _animalBasicInfoToAddGroup;

        

        private string _medicineToAddName;
        private string _medicineToAddManufacturer;
        private string _medicineToAddDose;
        private string _customerToAddFirstName;
        private string _customerToAddLastName;
        private string _customerToAddCity;
        private string _customerToAddPostalCode;
        private string _customerToAddStreet;
        private uint _customerToAddHouseNumber;
        private string _customerToAddContact;
        private string _animalToAddName;
        private string _animalToAddSpecies;
        private string _appointmentDescription;

        private bool _showOwnedAnimalsChecked;
        private bool _showAppointmentsHistoryChecked;


        private Customer _selectedCustomer;
        private AnimalBasicInfo _selectedAnimalBasicInfo;
        private Medicine _selectedMedicine;
        private Animal _selectedAnimal;
        private Animal _selectedAnimalInAppointmentTab;
        private Customer _selectedCustomerInAppointmentTab;

        private Gender _animalToAddGender;
        private PurposeOfVisit _appointmentPurposeOfVisit;
        private DateTime _appointmentDate;
        #endregion

        #region Properties
        public string AnimalBasicInfoToAddSpecies
        {
            get => _animalBasicInfoToAddSpecies;
            set
            {
                if(value != _animalBasicInfoToAddSpecies)
                {
                    _animalBasicInfoToAddSpecies = value;
                    RaisePropertyChanged(() => AnimalBasicInfoToAddSpecies);
                }
            }
        }

        public AnimalGroup AnimalBasicInfoToAddGroup
        {
            get => _animalBasicInfoToAddGroup;
            set
            {
                if (value != _animalBasicInfoToAddGroup)
                {
                    _animalBasicInfoToAddGroup = value;
                    RaisePropertyChanged(() => AnimalBasicInfoToAddGroup);
                }
            }
        }


        public string MedicineToAddName
        {
            get => _medicineToAddName;
            set
            {
                if(_medicineToAddName != value)
                {
                    _medicineToAddName = value;
                    RaisePropertyChanged(() => MedicineToAddName);
                }
            }
        }

        public string MedicineToAddManufacturer
        {
            get => _medicineToAddManufacturer;
            set
            {
                if (_medicineToAddManufacturer != value)
                {
                    _medicineToAddManufacturer = value;
                    RaisePropertyChanged(() => MedicineToAddManufacturer);
                }
            }
        }
        public string MedicineToAddDose
        {
            get => _medicineToAddDose;
            set
            {
                if (_medicineToAddDose != value)
                {
                    _medicineToAddDose = value;
                    RaisePropertyChanged(() => MedicineToAddDose);
                }
            }
        }

        public string CustomerToAddFirstName
        {
            get => _customerToAddFirstName;
            set
            {
                if(_customerToAddFirstName != value)
                {
                    _customerToAddFirstName = value;
                    RaisePropertyChanged(() => CustomerToAddFirstName);
                }
            }
        }
        public string CustomerToAddLastName
        {
            get => _customerToAddLastName;
            set
            {
                if (_customerToAddLastName != value)
                {
                    _customerToAddLastName = value;
                    RaisePropertyChanged(() => CustomerToAddLastName);
                }
            }
        }
        public string CustomerToAddCity
        {
            get => _customerToAddCity;
            set
            {
                if (_customerToAddCity != value)
                {
                    _customerToAddCity = value;
                    RaisePropertyChanged(() => CustomerToAddCity);
                }
            }
        }
        public string CustomerToAddPostalCode
        {
            get => _customerToAddPostalCode;
            set
            {
                if (_customerToAddPostalCode != value)
                {
                    _customerToAddPostalCode = value;
                    RaisePropertyChanged(() => CustomerToAddPostalCode);
                }
            }
        }
        public string CustomerToAddStreet
        {
            get => _customerToAddStreet;
            set
            {
                if (_customerToAddStreet != value)
                {
                    _customerToAddStreet = value;
                    RaisePropertyChanged(() => CustomerToAddStreet);
                }
            }
        }
        public uint CustomerToAddHouseNumber
        {
            get => _customerToAddHouseNumber;
            set
            {
                if (_customerToAddHouseNumber != value)
                {
                    _customerToAddHouseNumber = value;
                    RaisePropertyChanged(() => CustomerToAddHouseNumber);
                }
            }
        }
        public string CustomerToAddContact
        {
            get => _customerToAddContact;
            set
            {
                if (_customerToAddContact != value)
                {
                    _customerToAddContact = value;
                    RaisePropertyChanged(() => CustomerToAddContact);
                }
            }
        }

        public string AnimalToAddName
        {
            get => _animalToAddName;
            set
            {
                if(_animalToAddName != value)
                {
                    _animalToAddName = value;
                    RaisePropertyChanged(() => AnimalToAddName);
                }
            }
        }
        public string AnimalToAddSpecies 
        {
            get => _animalToAddSpecies;
            set
            {
                if (_animalToAddSpecies != value)
                {
                    _animalToAddSpecies = value;
                    RaisePropertyChanged(() => AnimalToAddSpecies);
                }
            }
        }
        public string AppointmentDescription
        {
            get => _appointmentDescription;
            set
            {
                if(_appointmentDescription != value)
                {
                    _appointmentDescription = value;
                    RaisePropertyChanged(() => AppointmentDescription);
                }
            }
        }

        public bool ShowOwnedAnimalsChecked
        {
            get => _showOwnedAnimalsChecked;
            set
            {
                if (_showOwnedAnimalsChecked != value)
                {
                    _showOwnedAnimalsChecked = value;
                    RaisePropertyChanged(() => ShowOwnedAnimalsChecked);
                }
            }
        }
        public bool ShowAppointmentsHistoryChecked
        {
            get => _showAppointmentsHistoryChecked;
            set
            {
                if (_showAppointmentsHistoryChecked != value)
                {
                    _showAppointmentsHistoryChecked = value;
                    RaisePropertyChanged(() => ShowAppointmentsHistoryChecked);
                }
            }
        }
        

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged(() => SelectedCustomer);
            }
        }

        public AnimalBasicInfo SelectedAnimalBasicInfo
        {
            get => _selectedAnimalBasicInfo;
            set
            {
                if(_selectedAnimalBasicInfo != value)
                {
                    _selectedAnimalBasicInfo = value;
                    RaisePropertyChanged(() => SelectedAnimalBasicInfo);
                }
            }
        }

        public Medicine SelectedMedicine
        {
            get => _selectedMedicine;
            set
            {
                if (_selectedMedicine != value)
                {
                    _selectedMedicine = value;
                    RaisePropertyChanged(() => SelectedMedicine);
                }
            }
        }
        public Animal SelectedAnimal
        {
            get => _selectedAnimal;
            set
            {
                if (_selectedAnimal != value)
                {
                    _selectedAnimal = value;
                    RaisePropertyChanged(() => SelectedAnimal);
                }
            }
        }
        public Animal SelectedAnimalInAppointmentTab
        {
            get => _selectedAnimalInAppointmentTab;
            set
            {
                if (_selectedAnimalInAppointmentTab != value)
                {
                    _selectedAnimalInAppointmentTab = value;
                    RaisePropertyChanged(() => SelectedAnimalInAppointmentTab);
                }
            }
        }

        public Customer SelectedCustomerInAppointmentTab
        {
            get => _selectedCustomerInAppointmentTab;
            set
            {
                if (_selectedCustomerInAppointmentTab != value)
                {
                    _selectedCustomerInAppointmentTab = value;
                    RaisePropertyChanged(() => SelectedCustomerInAppointmentTab);
                }
            }
        }


        public Gender AnimalToAddGender
        {
            get => _animalToAddGender;
            set
            {
                if (_animalToAddGender != value)
                {
                    _animalToAddGender = value;
                    RaisePropertyChanged(() => AnimalToAddGender);
                }
            }
        }

        public PurposeOfVisit AppointmentPurposeOfVisit
        {
            get => _appointmentPurposeOfVisit;
            set
            {
                if (_appointmentPurposeOfVisit != value)
                {
                    _appointmentPurposeOfVisit = value;
                    RaisePropertyChanged(() => AppointmentPurposeOfVisit);
                }
            }
        }
        public DateTime AppointmentDate
        {
            get => _appointmentDate;
            set
            {
                if (_appointmentDate != value)
                {
                    _appointmentDate = value;
                    RaisePropertyChanged(() => AppointmentDate);
                }
            }
        }
        
        #endregion

        #region Command variables and properties

        private IAsyncCommand _removeSelectedCustomerAsyncCommand;
        private IAsyncCommand _showPrefillingDatabaseWindowAsyncCommand;
        private IAsyncCommand<IList> _addNewAnimalAsyncCommand;
        private IAsyncCommand _addNewMedicineAsyncCommand;
        private IAsyncCommand _removeAllAnimalsAsyncCommand;
        private IAsyncCommand _removeAllMedicinesAsyncCommand;
        private IAsyncCommand _removeSelectedAnimalBasicInfoAsyncCommand;
        private IAsyncCommand _removeSelectedMedicineAsyncCommand;
        private IAsyncCommand _removeAllCustomersAsyncCommand;


        public IAsyncCommand RemoveSelectedCustomerAsyncCommand
        {
            get => _removeSelectedCustomerAsyncCommand ?? new AsyncCommand(() => RemoveSelectedCustomerAsync());
        }
        public IAsyncCommand ShowPrefillingDatabaseWindowAsyncCommand
        {
            get => _showPrefillingDatabaseWindowAsyncCommand ?? new AsyncCommand(() => ShowPrefillingDatabaseWindowAsync());
        }
        public IAsyncCommand<IList> AddNewAnimalAsyncCommand
        {
            get => _addNewAnimalAsyncCommand ?? new AsyncCommand<IList>(AddNewAnimalAsync);
        }
        public IAsyncCommand AddNewMedicineAsyncCommand
        {
            get => _addNewMedicineAsyncCommand ?? new AsyncCommand(() => AddNewMedicineAsync());
        }
        public IAsyncCommand RemoveAllAnimalsAsyncCommand
        {
            get => _removeAllAnimalsAsyncCommand ?? new AsyncCommand(() => RemoveAllAnimalsAsync());
        }
        public IAsyncCommand RemoveAllMedicinesAsyncCommand
        {
            get => _removeAllMedicinesAsyncCommand ?? new AsyncCommand(() => RemoveAllMedicinesAsync());
        }
        public IAsyncCommand RemoveSelectedAnimalBasicInfoAsyncCommand
        {
            get => _removeSelectedAnimalBasicInfoAsyncCommand ?? new AsyncCommand(() => RemoveSelectedAnimalBasicInfoAsync());
        }
        public IAsyncCommand RemoveSelectedMedicineAsyncCommand
        {
            get => _removeSelectedMedicineAsyncCommand ?? new AsyncCommand(() => RemoveSelectedMedicineAsync());
        }
        public IAsyncCommand RemoveAllCustomersAsyncCommand
        {
            get => _removeAllCustomersAsyncCommand ?? new AsyncCommand(() => RemoveAllCustomersAsync());
        }
        #endregion

        #region Collections
        // Collections 
        private ObservableCollection<Customer> _customers;

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

        public ObservableCollection<Animal> TreatedAnimals
        {
            get
            {
                using (var uow = new UnitOfWork())
                {
                    return new ObservableCollection<Animal>(uow.Animals.All);
                }
            }
        }

        public ObservableCollection<Animal> AnimalsTreatedInThePast { get; set; }

        public ObservableCollection<AnimalBasicInfo> AnimalBasicInfos
        {
            get
            {
                using (var uow = new UnitOfWork())
                {
                    return new ObservableCollection<AnimalBasicInfo>(uow.AnimalBasicInfos.All);
                }
            }
        }

        public ObservableCollection<Medicine> Medicines
        {
            get
            {
                using (var uow = new UnitOfWork())
                {
                    return new ObservableCollection<Medicine>(uow.Medicines.All);
                }
            }
        }
        #endregion

        #region Dependency properties
        public class MultiSelectBindableDataGrid : DataGrid
        {
            public static readonly DependencyProperty SelectedItemsProperty =
                DependencyProperty.Register("SelectedItems", typeof(IList), typeof(MultiSelectBindableDataGrid), new PropertyMetadata(default(IList)));

            public new IList SelectedItems
            {
                get { return (IList)GetValue(SelectedItemsProperty); }
                set { throw new Exception("This property is read-only. To bind to it you must use 'Mode=OneWayToSource'."); }
            }

            protected override void OnSelectionChanged(SelectionChangedEventArgs e)
            {
                base.OnSelectionChanged(e);
                SetValue(SelectedItemsProperty, base.SelectedItems);
            }
        }
        #endregion

        #region Command action tasks

        private async Task AddNewAnimalAsync(IList selectedItems)
        {

            AnimalBasicInfo animalBasicInfo = new AnimalBasicInfo();
            animalBasicInfo.Species = AnimalBasicInfoToAddSpecies;
            animalBasicInfo.Group = AnimalBasicInfoToAddGroup;
            var selectedMedicinesList = selectedItems.Cast<Medicine>();
            animalBasicInfo.AvailableMedicines = new ObservableCollection<Medicine>();

            using (var uow = new UnitOfWork())
            {
                foreach (var medicine in selectedMedicinesList)
                {
                    var medicineToAdd = uow.Medicines.Get(medicine.Id);
                    animalBasicInfo.AvailableMedicines.Add(medicineToAdd);
                    medicineToAdd.AssignedAnimals.Add(animalBasicInfo);
                }

                uow.AnimalBasicInfos.Add(animalBasicInfo);
                uow.Save();
            }

            RaisePropertyChanged(() => AnimalBasicInfos);
        }

        private async Task AddNewMedicineAsync()
        {
            Medicine medicine = new Medicine();
            medicine.Name = MedicineToAddName;
            medicine.Manufacturer = MedicineToAddManufacturer;
            medicine.Dose = MedicineToAddDose;

            using (var uow = new UnitOfWork())
            {
                uow.Medicines.Add(medicine);
                uow.Save();
            }

            RaisePropertyChanged(() => Medicines);
            MedicineToAddName = "";
            MedicineToAddManufacturer = "";
            MedicineToAddDose = "";
        }

        private async Task RemoveAllAnimalsAsync()
        {
            using (var uow = new UnitOfWork())
            {
                uow.AnimalBasicInfos.DeleteAll();

                uow.Save();
            }

            RaisePropertyChanged(() => AnimalBasicInfos);
            SelectedAnimalBasicInfo = null;
        }
        
        private async Task RemoveAllMedicinesAsync()
        {
            using (var uow = new UnitOfWork())
            {
                uow.Medicines.DeleteAll();

                uow.Save();
            }

            RaisePropertyChanged(() => Medicines);
            RaisePropertyChanged(() => AnimalBasicInfos);
        }

        private async Task RemoveSelectedAnimalBasicInfoAsync()
        {
            await Task.Run(() =>
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    if (SelectedAnimalBasicInfo == null)
                        return;

                    unitOfWork.AnimalBasicInfos.Delete(SelectedAnimalBasicInfo.Species);

                    unitOfWork.Save();
                }

            });

            SelectedAnimalBasicInfo = null;
            RaisePropertyChanged(() => AnimalBasicInfos);
        }
        private async Task RemoveSelectedMedicineAsync()
        {
            await Task.Run(() =>
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    if (SelectedMedicine == null)
                        return;

                    unitOfWork.Medicines.Delete(SelectedMedicine.Id);

                    unitOfWork.Save();
                }

            });

            SelectedMedicine = null;
            RaisePropertyChanged(() => Medicines);
            RaisePropertyChanged(() => AnimalBasicInfos);
        }

        private async Task RemoveSelectedCustomerAsync()
        {
            await Task.Run(() =>
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    if (SelectedCustomer == null)
                        return;

                    unitOfWork.Customers.Delete(SelectedCustomer.Id);

                    unitOfWork.Save();
                }

            });

            SelectedCustomer = null;
            RaisePropertyChanged(() => Customers);
            RaisePropertyChanged(() => TreatedAnimals);
            RaisePropertyChanged(() => Appointments);
        }

        private async Task RemoveAllCustomersAsync()
        {
            using (var uow = new UnitOfWork())
            {
                uow.Customers.DeleteAll();

                uow.Save();
            }

            SelectedCustomer = null;
            RaisePropertyChanged(() => Customers);
            RaisePropertyChanged(() => Appointments);
            RaisePropertyChanged(() => TreatedAnimals);
        }

        private async Task ShowPrefillingDatabaseWindowAsync()
        {
            if (!IsWindowOpen<Window>("PreliminaryDatabaseFillingWindow"))
            {
                PreliminaryDatabaseFillingWindow preliminaryDatabaseFillingWindow = new PreliminaryDatabaseFillingWindow();
                preliminaryDatabaseFillingWindow.Owner = System.Windows.Application.Current.MainWindow;
                preliminaryDatabaseFillingWindow.Show();
                preliminaryDatabaseFillingWindow.Name = "PreliminaryDatabaseFillingWindow";
            }
        }
        #endregion









        private ICommand _addNewCustomerAsyncCommand;

        public ICommand AddNewCustomerAsyncCommand
        {
            get => _addNewCustomerAsyncCommand ?? new RelayCommand(() => AddNewCustomer());
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
