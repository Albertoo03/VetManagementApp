using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
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
        #region Delegates 
        public delegate Task StatusOfAppointmentChangedEvent();
        #endregion

        #region Events
        public event StatusOfAppointmentChangedEvent RaiseStatusOfAppointmentChangedEvent;
        #endregion

        #region Private variables
        private string _animalBasicInfoToAddSpecies;
        private AnimalGroup _animalBasicInfoToAddGroup;

        

        private string _medicineToAddName;
        private string _medicineToAddManufacturer;
        private string _medicineToAddDose;
        private string _medicineToAddTargetAnimal;
        private string _customerToAddFirstName;
        private string _customerToAddLastName;
        private string _customerToAddCity;
        private string _customerToAddPostalCode;
        private string _customerToAddStreet;
        private int _customerToAddHouseNumber;
        private string _customerToAddContact;
        private string _animalToAddName;
        private AnimalBasicInfo _animalToAddSpecies;
        private string _appointmentDescription;

        private bool _showOwnedAnimalsChecked;
        private bool _showAppointmentsHistoryChecked;
        private bool _addNewCustomerIsChecked = true;
        private bool _customerFromDatabaseIsChecked;
        private bool _addNewAnimalIsChecked = true;
        private bool _animalFromDatabaseIsChecked;
        private bool _appointmentStatusWasChanged = false;

        private Customer _selectedCustomer;
        private AnimalBasicInfo _selectedAnimalBasicInfo;
        private Medicine _selectedMedicine;
        private Animal _selectedAnimal;
        private Animal _selectedAnimalInAppointmentTab;
        private Customer _selectedCustomerInAppointmentTab;

        private Customer _appointmentCustomer;
        private Animal _appointmentAnimal;
        private Appointment _selectedAppointment;

        private Gender _animalToAddGender;
        private PurposeOfVisit _appointmentPurposeOfVisit;
        private DateTime _appointmentDate = DateTime.Now;
        private StateOfVisit _selectedStatusOfVisit = StateOfVisit.WaitingForVisit;
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
        public string MedicineToAddTargetAnimal
        {
            get => _medicineToAddTargetAnimal;
            set
            {
                if (_medicineToAddTargetAnimal != value)
                {
                    _medicineToAddTargetAnimal = value;
                    RaisePropertyChanged(() => MedicineToAddTargetAnimal);
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
        public int CustomerToAddHouseNumber
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
        public AnimalBasicInfo AnimalToAddSpecies 
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
        
        public bool AddNewCustomerIsChecked
        {
            get => _addNewCustomerIsChecked;
            set
            {
                if(_addNewCustomerIsChecked != value)
                {
                    _addNewCustomerIsChecked = value;
                    RaisePropertyChanged(() => AddNewCustomerIsChecked);
                }
            }
        }

        public bool CustomerFromDatabaseIsChecked
        {
            get => _customerFromDatabaseIsChecked;
            set
            {
                if (_customerFromDatabaseIsChecked != value)
                {
                    _customerFromDatabaseIsChecked = value;
                    RaisePropertyChanged(() => CustomerFromDatabaseIsChecked);
                }
            }
        }

        public bool AddNewAnimalIsChecked
        {
            get => _addNewAnimalIsChecked;
            set
            {
                if (_addNewAnimalIsChecked != value)
                {
                    _addNewAnimalIsChecked = value;
                    RaisePropertyChanged(() => AddNewAnimalIsChecked);
                }
            }
        }

        public bool AnimalFromDatabaseIsChecked
        {
            get => _animalFromDatabaseIsChecked;
            set
            {
                if (_animalFromDatabaseIsChecked != value)
                {
                    _animalFromDatabaseIsChecked = value;
                    RaisePropertyChanged(() => AnimalFromDatabaseIsChecked);
                }
            }
        }
        public bool AppointmentStatusWasChanged
        {
            get => _appointmentStatusWasChanged;
            set
            {
                if (_appointmentStatusWasChanged != value)
                {
                    _appointmentStatusWasChanged = value;
                    RaisePropertyChanged(() => AppointmentStatusWasChanged);
                }
            }
        }

        
        public bool NewCustomerAllFieldsFilled
        {
            get 
            {
                if (CustomerToAddFirstName.Equals("") || CustomerToAddLastName.Equals("") || CustomerToAddCity.Equals("") || CustomerToAddPostalCode.Equals("")
                    || CustomerToAddStreet.Equals("") || CustomerToAddHouseNumber.Equals(0) || CustomerToAddContact.Equals(""))
                    return false;
                else
                    return true;
            }
        }
        public bool NewAnimalAllFieldsFilled
        {
            get
            {
                if (AnimalToAddName.Equals("") || AnimalToAddSpecies.Species.Equals(""))
                    return false;
                else
                    return true;
            }
        }

        public bool AppointmentsInfoAllFieldsFilled
        {
            get
            {
                if (AppointmentDate == null || AppointmentDescription == null)
                    return false;
                else
                    return true;
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


        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (_selectedAppointment != value)
                {
                    _selectedAppointment = value;
                    RaisePropertyChanged(() => SelectedAppointment);
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
        
        public StateOfVisit SelectedStatusOfVisit
        {
            get => _selectedStatusOfVisit;
            set
            {
                if(_selectedStatusOfVisit != value)
                {
                    _selectedStatusOfVisit = value;

                    if(SelectedAppointment != null)
                    {
                        RaiseStatusOfAppointmentChangedEvent();
                    }

                    RaisePropertyChanged(() => SelectedStatusOfVisit);
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
        private IAsyncCommand _makeAnAppointmentAsyncCommand;
        private IAsyncCommand _removeSelectedAppointmentAsyncCommand;
        private IAsyncCommand<Medicine> _leftMouseDoubleClickOnAvailableMedicinesAsyncCommand;
        private IAsyncCommand<Medicine> _leftMouseDoubleClickOnAssignedMedicinesAsyncCommand;

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
            get => _addNewAnimalAsyncCommand ?? new AsyncCommand<IList>(AddNewAnimalBasicInfoAsync);
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
        public IAsyncCommand MakeAnAppointmentAsyncCommand
        {
            get => _makeAnAppointmentAsyncCommand ?? new AsyncCommand(() => MakeAnAppointmentAsync());
        }
        public IAsyncCommand RemoveSelectedAppointmentAsyncCommand
        {
            get => _removeSelectedAppointmentAsyncCommand ?? new AsyncCommand(() => RemoveSelectedAppointmentAsync());
        }
        public IAsyncCommand<Medicine> LeftMouseDoubleClickOnAvailableMedicinesAsyncCommand
        {
            get => _leftMouseDoubleClickOnAvailableMedicinesAsyncCommand ?? new AsyncCommand<Medicine>(LeftMouseDoubleOnAvailableMedicinesClickAsync);
        }
        public IAsyncCommand<Medicine> LeftMouseDoubleClickOnAssignedMedicinesAsyncCommand
        {
            get => _leftMouseDoubleClickOnAssignedMedicinesAsyncCommand ?? new AsyncCommand<Medicine>(LeftMouseDoubleOnAssignedMedicinesClickAsync);
        }

        #endregion

        #region Collections

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

        private async Task AddNewAnimalBasicInfoAsync(IList selectedItems)
        {
            
            AnimalBasicInfo animalBasicInfo = new AnimalBasicInfo();
            animalBasicInfo.Species = AnimalBasicInfoToAddSpecies;
            animalBasicInfo.Group = AnimalBasicInfoToAddGroup;
            var selectedMedicinesList = selectedItems.Cast<Medicine>();
            animalBasicInfo.AvailableMedicines = new ObservableCollection<Medicine>();
            animalBasicInfo.AssignedAnimals = new ObservableCollection<Animal>();

            try
            {
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
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is already an animals with the same species in the database!");
                return;
            }


            RaisePropertyChanged(() => AnimalBasicInfos);
        }

        private async Task AddNewMedicineAsync()
        {
            Medicine medicine = new Medicine();
            medicine.Name = MedicineToAddName;
            medicine.Manufacturer = MedicineToAddManufacturer;
            medicine.Dose = MedicineToAddDose;
            medicine.TargetAnimal = MedicineToAddTargetAnimal;

            using (var uow = new UnitOfWork())
            {
                var medFromDb = uow.Medicines.GetByPredicate(med => med.Name == medicine.Name);

                if(medFromDb != null)
                {
                    MessageBox.Show("There is already medicine with the same name in database!");
                    return;
                }

                uow.Medicines.Add(medicine);
                var animalToAssign = uow.AnimalBasicInfos.GetByPredicate(animal => animal.Species == medicine.TargetAnimal);

                animalToAssign.AvailableMedicines.Add(medicine);

                uow.Save();
            }

            RaisePropertyChanged(() => Medicines);
            RaisePropertyChanged(() => AnimalBasicInfos);

            MedicineToAddName = "";
            MedicineToAddManufacturer = "";
            MedicineToAddDose = "";
            MedicineToAddTargetAnimal = "";
        }

        private async Task RemoveAllAnimalsAsync()
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    uow.AnimalBasicInfos.DeleteAll();

                    uow.Save();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine("==================");

                MessageBox.Show("You cannot delete all animals types because some of them are assigned to treated animal.");
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
            RaisePropertyChanged(() => TreatedAnimals);
        }

        private async Task RemoveSelectedAnimalBasicInfoAsync()
        {
            await Task.Run(() =>
            {
                try
                {
               
                        using (var unitOfWork = new UnitOfWork())
                        {
                            if (SelectedAnimalBasicInfo == null)
                                return;

                            unitOfWork.AnimalBasicInfos.Delete(SelectedAnimalBasicInfo.Species);

                            unitOfWork.Save();
                        }

                    
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("==================");
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine("==================");
                    Debug.WriteLine(ex.StackTrace);
                    Debug.WriteLine("==================");
                    Debug.WriteLine(ex.InnerException);
                    Debug.WriteLine("==================");

                    MessageBox.Show("You cannot delete this type because it is assigned to treated animal.");

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



        private async Task MakeAnAppointmentAsync()
        {

            bool allConditionsMet = await CheckIfAllConditionsToMakeAppointmentAreMet();

            if (allConditionsMet == false)
            {
                MessageBox.Show("Fill out all fields in the form first!");
                return;
            }


            var appointmentCustomer = new Customer();
            var appointmentAnimal = new Animal();

            if (AddNewCustomerIsChecked)
            {
                appointmentCustomer.FirstName = CustomerToAddFirstName;
                appointmentCustomer.LastName = CustomerToAddLastName;
                appointmentCustomer.City = CustomerToAddCity;
                appointmentCustomer.PostalCode = CustomerToAddPostalCode;
                appointmentCustomer.Street = CustomerToAddStreet;
                appointmentCustomer.HouseNumber = CustomerToAddHouseNumber;
                appointmentCustomer.Contact = CustomerToAddContact;

                appointmentAnimal.Name = AnimalToAddName;
                appointmentAnimal.Gender = AnimalToAddGender;

                appointmentCustomer.OwnedAnimals = new ObservableCollection<Animal>();
                appointmentCustomer.OwnedAnimals.Add(appointmentAnimal);

                appointmentAnimal.Owner = appointmentCustomer;
            }


            if (CustomerFromDatabaseIsChecked)
            {
                appointmentCustomer = SelectedCustomerInAppointmentTab;

                if (AddNewAnimalIsChecked)
                {
                    appointmentAnimal.Name = AnimalToAddName;
                    appointmentAnimal.Gender = AnimalToAddGender;

                    appointmentCustomer.OwnedAnimals.Add(appointmentAnimal);
                    appointmentAnimal.Owner = appointmentCustomer;
                    

                }

                if (AnimalFromDatabaseIsChecked)
                {
                    appointmentAnimal = SelectedAnimalInAppointmentTab;

                    appointmentCustomer.OwnedAnimals.Add(appointmentAnimal);
                }
            }



            Appointment appointment = new Appointment();
            appointment.AppointedAnimal = appointmentAnimal;
            appointment.AppointedCustomer = appointmentCustomer;
            appointment.Date = AppointmentDate;
            appointment.Description = AppointmentDescription;
            appointment.PurposeOfVisit = AppointmentPurposeOfVisit;
            appointment.StateOfVisit = StateOfVisit.WaitingForVisit;

            try
            {
                using (var uow = new UnitOfWork())
                {
                    if(CustomerFromDatabaseIsChecked)
                    {
                        uow.Customers.SetAsUnchanged(appointmentCustomer);
                    }

                    if(AnimalFromDatabaseIsChecked)
                    {
                        uow.Animals.SetAsUnchanged(appointmentAnimal);
                    }

                    var listOfAppointments = uow.Customers.GetAppointments(appointmentCustomer);

                    if (listOfAppointments.Count() < 1)
                        appointmentCustomer.Appointments = new ObservableCollection<Appointment>();
                    else
                        appointmentCustomer.Appointments = listOfAppointments;

                    appointmentCustomer.Appointments.Add(appointment);


                    var listOfAnimalAppointments = uow.Animals.GetAppointments(appointmentAnimal);

                    if (listOfAnimalAppointments.Count() < 1)
                        appointmentAnimal.Appointments = new ObservableCollection<Appointment>(listOfAnimalAppointments);
                    else
                        appointmentAnimal.Appointments = listOfAnimalAppointments;


                    appointmentAnimal.Appointments.Add(appointment);


                    if (AddNewAnimalIsChecked)
                    {
                        uow.Animals.SetAsAdded(appointmentAnimal);

                        var animalBasicInfo = uow.AnimalBasicInfos.GetBySpecies(AnimalToAddSpecies.Species);

                        
                        //animalBasicInfo.AssignedAnimals = new ObservableCollection<Animal>();

                        //animalBasicInfo.AssignedAnimals.Add(appointmentAnimal);
                        appointmentAnimal.SpeciesInfo = animalBasicInfo;
                        //var basicInfo = animalBasicInfo.AssignedAnimals.Where(animal => animal.Id == appointmentAnimal.Id).FirstOrDefault().SpeciesInfo;

                        //uow.AnimalBasicInfos.SetAsUnchanged(basicInfo);
                        //uow.Animals.SetAsUnchanged(appointmentAnimal);
                        //uow.AnimalBasicInfos.SetAsUnchanged(animalBasicInfo);
                    }
                    

                    

                    //var appointCustomer = uow.Appointments.Get(appointment.Id).AppointedCustomer;
                    //var appointAnimal = uow.Appointments.Get(appointment.Id).AppointedAnimal;
                    //uow.Customers.Add(appointmentCustomer);
                    //uow.Animals.Add(appointmentAnimal);
                    uow.Appointments.Add(appointment);
                    //var medicineToAdd = uow.Medicines.Get(medicine.Id);
                    //animalBasicInfo.AvailableMedicines.Add(medicineToAdd);
                    //medicineToAdd.AssignedAnimals.Add(animalBasicInfo);

                    uow.Save();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update original values from the database
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine("==================");
            }
           

            RaisePropertyChanged(() => Appointments);
            RaisePropertyChanged(() => Customers);
            RaisePropertyChanged(() => TreatedAnimals);

            // null values 
            CustomerToAddFirstName = null;
            CustomerToAddLastName = null;
            CustomerToAddCity = null;
            CustomerToAddPostalCode = null;
            CustomerToAddStreet = null;
            CustomerToAddHouseNumber = 0;
            CustomerToAddContact = null;

            AnimalToAddName = null;
            AnimalToAddGender = Gender.Female;

            SelectedCustomerInAppointmentTab = null;
            SelectedAnimalInAppointmentTab = null;

            AppointmentDate = DateTime.Now;
            AppointmentDescription = null;
            AppointmentPurposeOfVisit = PurposeOfVisit.FirstVisit;
        }

        private async Task<bool> CheckIfAllConditionsToMakeAppointmentAreMet()
        {
            bool customerConditionsMet = false;
            bool animalConditionsMet = false;
            bool appointmentInfoConditionsMet = false;
            bool allConditionsMet = false;


            if (AddNewCustomerIsChecked)
            {
                customerConditionsMet = NewCustomerAllFieldsFilled;
                animalConditionsMet = NewAnimalAllFieldsFilled;
            }

            if (CustomerFromDatabaseIsChecked)
            {

                if (SelectedCustomerInAppointmentTab != null)
                    customerConditionsMet = true;

                if (AddNewAnimalIsChecked)
                {
                    animalConditionsMet = NewAnimalAllFieldsFilled;
                }

                if (AnimalFromDatabaseIsChecked)
                {
                    if (SelectedAnimalInAppointmentTab != null)
                        animalConditionsMet = true;
                }
            }

            appointmentInfoConditionsMet = AppointmentsInfoAllFieldsFilled;


            allConditionsMet = (customerConditionsMet && animalConditionsMet && appointmentInfoConditionsMet) ? true : false;

            return allConditionsMet;
        }

        private async Task RemoveSelectedCustomerAsync()
        {
            await Task.Run(() =>
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    if (SelectedCustomer == null)
                        return;

                    unitOfWork.Animals.Delete(a => a.Owner.Id == SelectedCustomer.Id);
                    unitOfWork.Appointments.Delete(a => a.AppointedCustomer.Id == SelectedCustomer.Id);
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

                var customers = uow.Customers.GetAll();

                foreach(var customer in customers)
                {
                    uow.Animals.Delete(a => a.Owner.Id == customer.Id);
                    uow.Appointments.Delete(a => a.AppointedCustomer.Id == customer.Id);
                    uow.Customers.Delete(customer.Id);
                }

                uow.Save();
            }

            SelectedCustomer = null;
            RaisePropertyChanged(() => Customers);
            RaisePropertyChanged(() => Appointments);
            RaisePropertyChanged(() => TreatedAnimals);
        }

        private async Task RemoveSelectedAppointmentAsync()
        {
            await Task.Run(() =>
            {
                try
                {

                    using (var unitOfWork = new UnitOfWork())
                    {
                        if (SelectedAppointment == null)
                            return;

                        var appointmentCustomer = unitOfWork.Customers.Get(SelectedAppointment.AppointedCustomer.Id);
                        appointmentCustomer.Appointments.Remove(SelectedAppointment);

                        var appointmentAnimal = unitOfWork.Animals.Get(SelectedAppointment.AppointedAnimal.Id);

                        appointmentAnimal.Appointments.Remove(SelectedAppointment);

                        unitOfWork.Appointments.Delete(SelectedAppointment.Id);

                        unitOfWork.Save();
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("==================");
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine("==================");
                    Debug.WriteLine(ex.StackTrace);
                    Debug.WriteLine("==================");
                    Debug.WriteLine(ex.InnerException);
                    Debug.WriteLine("==================");

                    MessageBox.Show("You cannot delete this type because it is assigned to treated animal.");

                }
            });

            SelectedAppointment = null;
            RaisePropertyChanged(() => Appointments);
        }

        private async Task ShowPrefillingDatabaseWindowAsync()
        {
            if (!IsWindowOpen<Window>("PreliminaryDatabaseFillingWindow"))
            {
                PreliminaryDatabaseFillingWindow preliminaryDatabaseFillingWindow = new PreliminaryDatabaseFillingWindow();
                preliminaryDatabaseFillingWindow.Owner = System.Windows.Application.Current.MainWindow;
                preliminaryDatabaseFillingWindow.ShowDialog();
                preliminaryDatabaseFillingWindow.Name = "PreliminaryDatabaseFillingWindow";
            }
        }

        private async Task LeftMouseDoubleOnAvailableMedicinesClickAsync(Medicine selectedMedicine)
        {
            if (selectedMedicine == null)
                return;

            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var animal = unitOfWork.Animals.Get(SelectedAppointment.AppointedAnimal.Id);
                    var medicine = unitOfWork.Medicines.Get(selectedMedicine.Id);


                    animal.AssignedMedicines.Add(medicine);

                    unitOfWork.Save();
                }

                RaisePropertyChanged(() => Appointments);
                RaisePropertyChanged(() => TreatedAnimals);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine("==================");


            }

            
        }

        private async Task LeftMouseDoubleOnAssignedMedicinesClickAsync(Medicine selectedMedicine)
        {

            if (selectedMedicine == null)
                return;

            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var animal = unitOfWork.Animals.Get(SelectedAppointment.AppointedAnimal.Id);
                    var medicine = unitOfWork.Medicines.Get(selectedMedicine.Id);


                    animal.AssignedMedicines.Remove(medicine);

                    unitOfWork.Save();
                }

                RaisePropertyChanged(() => Appointments);
                RaisePropertyChanged(() => TreatedAnimals);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("==================");
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine("==================");


            }
        }
        #endregion

        #region Event callbacks
        public async Task OnRaiseStatusOfAppointmentChangedEvent()
        {
            if (SelectedAppointment == null)
                return;

            using (var uow = new UnitOfWork())
            {
                var selectedAppointment = uow.Appointments.Get(SelectedAppointment.Id);

                selectedAppointment.StateOfVisit = SelectedStatusOfVisit;

                uow.Save();
            }

            RaisePropertyChanged(() => Appointments);

        }
        #endregion

        public MainViewModel()
        {
            // Add callback to the event handler
            RaiseStatusOfAppointmentChangedEvent += OnRaiseStatusOfAppointmentChangedEvent;
        }


    }

}
