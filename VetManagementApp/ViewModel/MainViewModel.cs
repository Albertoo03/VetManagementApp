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
        public Func<string, Task> RaiseFilterAppointmentChangedEvent;
        #endregion

        #region Private variables

        private Customer _selectedCustomer;
        private AnimalBasicInfo _selectedAnimalBasicInfo;
        private Medicine _selectedMedicine;
        private Animal _selectedAnimal;
        private Animal _selectedAnimalInAppointmentTab;
        private Customer _selectedCustomerInAppointmentTab;
        private Appointment _selectedAppointment;
        private AnimalBasicInfo _animalToAddSpecies;

        private AnimalGroup _animalBasicInfoToAddGroup;
        private Gender _animalToAddGender;
        private PurposeOfVisit _appointmentPurposeOfVisit;
        private DateTime _appointmentDate = DateTime.Now;
        private StateOfVisit _selectedStatusOfVisit = StateOfVisit.WaitingForVisit;

        private const string _showAllAppointments = "ShowAllAppointments";
        private const string _showPastAppointments = "ShowPastAppointments";
        private const string _showUpcomingAppointments = "ShowUpcomingAppointments";

        private string _animalBasicInfoToAddSpecies;
        private string _medicineToAddName;
        private string _medicineToAddManufacturer;
        private string _medicineToAddDose;
        private string _medicineToAddTargetAnimal;
        private string _customerToAddFirstName;
        private string _customerToAddLastName;
        private string _customerToAddCity;
        private string _customerToAddPostalCode;
        private string _customerToAddStreet;
        private int _customerToAddHouseNumber = 1;
        private string _customerToAddContact;
        private string _animalToAddName;
        private string _appointmentDescription;
        private string _currentlySelectedFilterActionOnAppointmentTab = _showAllAppointments;
        private string _logsToShow;

        private bool _showOwnedAnimalsChecked;
        private bool _showAppointmentsHistoryChecked;
        private bool _addNewCustomerIsChecked = true;
        private bool _customerFromDatabaseIsChecked;
        private bool _addNewAnimalIsChecked = true;
        private bool _animalFromDatabaseIsChecked;
        private bool _appointmentStatusWasChanged = false;
        private bool _showAllAppointmentsIsChecked = false;
        private bool _showOnlyPastAppointmentsIsChecked = false;
        private bool _showOnlyUpcomingAppointmentsIsChecked = false;
        private bool _autoScrollLogs = true;

        #endregion

        #region Properties

        public StringBuilder Logs { get; set; }

        public string LogsToShow
        {
            get
            {
                return _logsToShow;
            }
            set
            {
                if (_logsToShow != value)
                {
                    _logsToShow = value;
                    RaisePropertyChanged(() => LogsToShow);
                }
            }
        }

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

        public bool ShowAllAppointmentsIsChecked
        {
            get => _showAllAppointmentsIsChecked;
            set
            {
                if (_showAllAppointmentsIsChecked != value)
                {
                    _showAllAppointmentsIsChecked = value;

                    if (_showAllAppointmentsIsChecked == true)
                    {
                        _currentlySelectedFilterActionOnAppointmentTab = _showAllAppointments;
                        RaiseFilterAppointmentChangedEvent(_showAllAppointments);
                    }
                        

                    if(!_showAllAppointmentsIsChecked && !_showOnlyPastAppointmentsIsChecked && !_showOnlyUpcomingAppointmentsIsChecked)
                    {
                        _currentlySelectedFilterActionOnAppointmentTab = _showAllAppointments;
                        RaiseFilterAppointmentChangedEvent(_currentlySelectedFilterActionOnAppointmentTab);
                    }

                    RaisePropertyChanged(() => ShowAllAppointmentsIsChecked);
                }
            }
        }
        public bool ShowOnlyPastAppointmentsIsChecked
        {
            get => _showOnlyPastAppointmentsIsChecked;
            set
            {
                if (_showOnlyPastAppointmentsIsChecked != value)
                {
                    _showOnlyPastAppointmentsIsChecked = value;

                    if (_showOnlyPastAppointmentsIsChecked == true)
                    {
                        _currentlySelectedFilterActionOnAppointmentTab = _showPastAppointments;
                        RaiseFilterAppointmentChangedEvent(_showPastAppointments);
                    }
                        

                    if (!_showAllAppointmentsIsChecked && !_showOnlyPastAppointmentsIsChecked && !_showOnlyUpcomingAppointmentsIsChecked)
                    {
                        _currentlySelectedFilterActionOnAppointmentTab = _showAllAppointments;
                        RaiseFilterAppointmentChangedEvent(_currentlySelectedFilterActionOnAppointmentTab);
                    }

                    RaisePropertyChanged(() => ShowOnlyPastAppointmentsIsChecked);
                }
            }
        }
        public bool ShowOnlyUpcomingAppointmentsIsChecked
        {
            get => _showOnlyUpcomingAppointmentsIsChecked;
            set
            {
                if (_showOnlyUpcomingAppointmentsIsChecked != value)
                {
                    _showOnlyUpcomingAppointmentsIsChecked = value;

                    if (_showOnlyUpcomingAppointmentsIsChecked == true)
                    {
                        _currentlySelectedFilterActionOnAppointmentTab = _showUpcomingAppointments;
                        RaiseFilterAppointmentChangedEvent(_showUpcomingAppointments);
                    }
                        

                    if (!_showAllAppointmentsIsChecked && !_showOnlyPastAppointmentsIsChecked && !_showOnlyUpcomingAppointmentsIsChecked)
                    {
                        _currentlySelectedFilterActionOnAppointmentTab = _showAllAppointments;
                        RaiseFilterAppointmentChangedEvent(_currentlySelectedFilterActionOnAppointmentTab);
                    }
                        

                    RaisePropertyChanged(() => ShowOnlyUpcomingAppointmentsIsChecked);
                }
            }
        }
        
        /// <summary>
        /// Property informing if all fields of new customer form are filled.
        /// </summary>
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

        /// <summary>
        /// Property informing if all fields of new animal form are filled.
        /// </summary>
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

        /// <summary>
        /// Property informing if all fields of appointment info form are filled.
        /// </summary>
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

        public bool AutoScrollLogs
        {
            get
            {
                return _autoScrollLogs;
            }
            set
            {
                _autoScrollLogs = value;
                RaisePropertyChanged(() => AutoScrollLogs);
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

        /// <summary>
        /// Property informing about status selected by user. Checks if selected status is different from selected appointment status.
        /// </summary>
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
                        if(SelectedAppointment.StateOfVisit != _selectedStatusOfVisit)
                            RaiseStatusOfAppointmentChangedEvent();
                    }

                    RaisePropertyChanged(() => SelectedStatusOfVisit);
                }
            }
        }
        #endregion

        #region Command variables and properties

        private AsyncCommand _removeSelectedCustomerAsyncCommand;
        private AsyncCommand _showPrefillingDatabaseWindowAsyncCommand;
        private AsyncCommand _openLogsWindowAsyncCommand;
        private AsyncCommand<IList> _addNewAnimalAsyncCommand;
        private AsyncCommand _addNewMedicineAsyncCommand;
        private AsyncCommand _removeAllAnimalBasicInfosAsyncCommand;
        private AsyncCommand _removeAllMedicinesAsyncCommand;
        private AsyncCommand _removeSelectedAnimalBasicInfoAsyncCommand;
        private AsyncCommand _removeSelectedMedicineAsyncCommand;
        private AsyncCommand _removeAllCustomersAsyncCommand;
        private AsyncCommand _makeAnAppointmentAsyncCommand;
        private AsyncCommand _removeSelectedAppointmentAsyncCommand;
        private AsyncCommand<Medicine> _leftMouseDoubleClickOnAvailableMedicinesAsyncCommand;
        private AsyncCommand<Medicine> _leftMouseDoubleClickOnAssignedMedicinesAsyncCommand;
        private AsyncCommand _autoScrollLogsAsyncCommand;
        private AsyncCommand _exitTheAppAsyncCommand;
        private AsyncCommand _rightMouseClickAsyncCommand;

        public IAsyncCommand RemoveSelectedCustomerAsyncCommand
        {
            get => _removeSelectedCustomerAsyncCommand ?? new AsyncCommand(() => RemoveSelectedCustomerAsync());
        }
        public IAsyncCommand ShowPrefillingDatabaseWindowAsyncCommand
        {
            get => _showPrefillingDatabaseWindowAsyncCommand ?? new AsyncCommand(() => ShowPrefillingDatabaseWindowAsync());
        }
        public IAsyncCommand OpenLogsWindowAsyncCommand
        {
            get => _openLogsWindowAsyncCommand ?? new AsyncCommand(() => OpenLogsWindowAsync());
        }
        public IAsyncCommand<IList> AddNewAnimalAsyncCommand
        {
            get => _addNewAnimalAsyncCommand ?? new AsyncCommand<IList>(AddNewAnimalBasicInfoAsync);
        }
        public IAsyncCommand AddNewMedicineAsyncCommand
        {
            get => _addNewMedicineAsyncCommand ?? new AsyncCommand(() => AddNewMedicineAsync());
        }
        public IAsyncCommand RemoveAllAnimalBasicInfosAsyncCommand
        {
            get => _removeAllAnimalBasicInfosAsyncCommand ?? new AsyncCommand(() => RemoveAllAnimalBasicInfosAsync());
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
        public IAsyncCommand AutoScrollLogsAsyncCommand
        {
            get =>_autoScrollLogsAsyncCommand ?? new AsyncCommand(() => AutoScrollLogsAsync());
            
        }
        public IAsyncCommand ExitTheAppAsyncCommand
        {
            get => _exitTheAppAsyncCommand ?? new AsyncCommand(() => ExitTheAppAsync());
        }
        public IAsyncCommand RightMouseClickAsyncCommand
        {
            get => _rightMouseClickAsyncCommand ?? new AsyncCommand(() => RightMouseClickAsync());
        }
        
        #endregion

        #region Collections
        private ObservableCollection<Appointment> _appointments;

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
            get => _appointments;
            set
            {
                if(_appointments != value)
                {
                    _appointments = value;

                    if(RaiseFilterAppointmentChangedEvent!= null)
                        RaiseFilterAppointmentChangedEvent(_currentlySelectedFilterActionOnAppointmentTab);
                }
            }
        }

        public ObservableCollection<Appointment> FilteredAppointments { get; set; }

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

        #region Command action tasks

        /// <summary>
        /// Adds new animal species to the database.
        /// </summary>
        /// <param name="selectedItems"></param>
        /// <returns></returns>
        private async Task AddNewAnimalBasicInfoAsync(IList selectedItems)
        {
            
            AnimalBasicInfo animalBasicInfo = new AnimalBasicInfo(AnimalBasicInfoToAddSpecies, AnimalBasicInfoToAddGroup);

            var selectedMedicinesList = selectedItems.Cast<Medicine>();
            animalBasicInfo.AvailableMedicines = new ObservableCollection<Medicine>();
            animalBasicInfo.AssignedAnimals = new ObservableCollection<Animal>();

            try
            {
                await Task.Run(() =>
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
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is already an animals with the same species in the database!");
                return;
            }


            RaisePropertyChanged(() => AnimalBasicInfos);
            SelectedAnimalBasicInfo = null;

            if(animalBasicInfo != null)
                await Task.Run(() => AppendLog($"New animal species '{animalBasicInfo.Species}' added to the database."));
        }


        /// <summary>
        /// Adds new medicine to the database.
        /// </summary>
        /// <returns></returns>
        private async Task AddNewMedicineAsync()
        {
            Medicine medicine = new Medicine(MedicineToAddName, MedicineToAddManufacturer, MedicineToAddDose, MedicineToAddTargetAnimal);

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

                Appointments = new ObservableCollection<Appointment>(uow.Appointments.GetAll());

                uow.Save();
            }

            RaisePropertyChanged(() => Medicines);
            RaisePropertyChanged(() => AnimalBasicInfos);
            RaisePropertyChanged(() => TreatedAnimals);

            MedicineToAddName = "";
            MedicineToAddManufacturer = "";
            MedicineToAddDose = "";
            MedicineToAddTargetAnimal = "";
            SelectedMedicine = null;

            if(medicine != null)
                await Task.Run(() => AppendLog($"New medicine '{medicine.Name}' added to the database."));
        }


        /// <summary>
        /// Removes all animal species from the database.
        /// </summary>
        /// <returns></returns>
        private async Task RemoveAllAnimalBasicInfosAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var uow = new UnitOfWork())
                    {
                        uow.AnimalBasicInfos.DeleteAll();

                        uow.Save();
                    }
                });
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


            SelectedAnimalBasicInfo = null;
            RaisePropertyChanged(() => AnimalBasicInfos);

            await Task.Run(() => AppendLog($"All animals have been removed from the database."));
        }
        

        /// <summary>
        /// Removes all medicines from the database.
        /// </summary>
        /// <returns></returns>
        private async Task RemoveAllMedicinesAsync()
        {
            await Task.Run(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    uow.Medicines.DeleteAll();

                    uow.Save();
                }
            });

            RaisePropertyChanged(() => Medicines);
            RaisePropertyChanged(() => AnimalBasicInfos);
            RaisePropertyChanged(() => TreatedAnimals);

            await Task.Run(() => AppendLog($"All medicines have been removed from the database."));
        }


        /// <summary>
        /// Removes selected animal species from the database.
        /// </summary>
        /// <returns></returns>
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


            if(SelectedAnimalBasicInfo != null)
                await Task.Run(() => AppendLog($"{SelectedAnimalBasicInfo.Species} species has been removed from the database."));

            SelectedAnimalBasicInfo = null;
            RaisePropertyChanged(() => AnimalBasicInfos);
        }


        /// <summary>
        /// Removes selected medicine from the database.
        /// </summary>
        /// <returns></returns>
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


            if(SelectedMedicine != null)
                await Task.Run(() => AppendLog($"{SelectedMedicine.Name} medicine has been removed from the database."));

            SelectedMedicine = null;
            RaisePropertyChanged(() => Medicines);
            RaisePropertyChanged(() => AnimalBasicInfos);
        }


        /// <summary>
        /// Makes an appointment based on filled form.
        /// </summary>
        /// <returns></returns>
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

            // create new customer and assign data to it
            if (AddNewCustomerIsChecked)
            {
                appointmentCustomer.FirstName = CustomerToAddFirstName;
                appointmentCustomer.LastName = CustomerToAddLastName;
                appointmentCustomer.City = CustomerToAddCity;
                appointmentCustomer.PostalCode = CustomerToAddPostalCode;
                appointmentCustomer.Street = CustomerToAddStreet;
                appointmentCustomer.HouseNumber = CustomerToAddHouseNumber;
                appointmentCustomer.Contact = CustomerToAddContact;
                
                // assign data to new animal
                appointmentAnimal.Name = AnimalToAddName;
                appointmentAnimal.Gender = AnimalToAddGender;

                appointmentCustomer.OwnedAnimals = new ObservableCollection<Animal>();
                appointmentCustomer.OwnedAnimals.Add(appointmentAnimal);

                appointmentAnimal.Owner = appointmentCustomer;
            }

            // customer selected from database
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
                        // inform EF that customer already is in database
                        uow.Customers.SetAsUnchanged(appointmentCustomer);
                    }

                    
                    if(AnimalFromDatabaseIsChecked)
                    {
                        // inform EF that animal is already in database
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

                        appointmentAnimal.SpeciesInfo = animalBasicInfo;
                    }
                    

                    uow.Appointments.Add(appointment);

                    Appointments = new ObservableCollection<Appointment>(uow.Appointments.GetAll());
                    
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
           

            RaisePropertyChanged(() => Customers);
            RaisePropertyChanged(() => TreatedAnimals);

            // null form values 
            await ResetAppointmentFormValues();
            

            await Task.Run(() => AppendLog($"New appointment has been scheduled."));
        }


        /// <summary>
        /// Removes selected customer from database.
        /// </summary>
        /// <returns></returns>
        private async Task RemoveSelectedCustomerAsync()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (SelectedCustomer == null)
                    return;

                unitOfWork.Animals.Delete(a => a.Owner.Id == SelectedCustomer.Id);
                unitOfWork.Appointments.Delete(a => a.AppointedCustomer.Id == SelectedCustomer.Id);
                unitOfWork.Customers.Delete(SelectedCustomer.Id);


                Appointments = new ObservableCollection<Appointment>(unitOfWork.Appointments.GetAll());

                unitOfWork.Save();
            }

            if(SelectedCustomer != null)
                await Task.Run(() => AppendLog($"{SelectedCustomer.FullName} has been removed from the database."));

            SelectedCustomer = null;
            RaisePropertyChanged(() => Customers);
            RaisePropertyChanged(() => TreatedAnimals);
        }


        /// <summary>
        /// Removes all customers from database.
        /// </summary>
        /// <returns></returns>
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

                Appointments = new ObservableCollection<Appointment>(uow.Appointments.GetAll());

                uow.Save();
            }

            SelectedCustomer = null;
            RaisePropertyChanged(() => Customers);
            RaisePropertyChanged(() => TreatedAnimals);

            await Task.Run(() => AppendLog($"All customers have been removed from the database."));
        }


        /// <summary>
        /// Removes selected appointment from database.
        /// </summary>
        /// <returns></returns>
        private async Task RemoveSelectedAppointmentAsync()
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


                    Appointments = new ObservableCollection<Appointment>(unitOfWork.Appointments.GetAll());

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

            SelectedAppointment = null;
            await Task.Run(() => AppendLog($"One of the appointments has been removed from the database."));
        }


        /// <summary>
        /// Opens prefilling database window.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Opens logs window.
        /// </summary>
        /// <returns></returns>
        private async Task OpenLogsWindowAsync()
        {
            if (!IsWindowOpen<Window>("LogsWindow"))
            {
                LogsWindow logsWindow = new LogsWindow();
                logsWindow.Owner = System.Windows.Application.Current.MainWindow;
                logsWindow.Show();
                logsWindow.Name = "LogsWindow";
            }
        }


        /// <summary>
        /// Left mouse double click on available medicines listview handler.
        /// </summary>
        /// <param name="selectedMedicine"></param>
        /// <returns></returns>
        private async Task LeftMouseDoubleOnAvailableMedicinesClickAsync(Medicine selectedMedicine)
        {
            int animalId = 0;

            if (selectedMedicine == null)
                return;

            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var animal = unitOfWork.Animals.Get(SelectedAppointment.AppointedAnimal.Id);
                    var medicine = unitOfWork.Medicines.Get(selectedMedicine.Id);

                    animalId = animal.Id;

                    animal.AssignedMedicines.Add(medicine);


                    Appointments = new ObservableCollection<Appointment>(unitOfWork.Appointments.GetAll());

                    unitOfWork.Save();
                }

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

            if(selectedMedicine != null)
                await Task.Run(() => AppendLog($"{selectedMedicine.Name} medicine has been assigned to animal with Id:{animalId}."));
        }


        /// <summary>
        /// Left mouse double click on assigned medicines listview handler.
        /// </summary>
        /// <param name="selectedMedicine"></param>
        /// <returns></returns>
        private async Task LeftMouseDoubleOnAssignedMedicinesClickAsync(Medicine selectedMedicine)
        {
            int animalId = 0;

            if (selectedMedicine == null)
                return;

            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var animal = unitOfWork.Animals.Get(SelectedAppointment.AppointedAnimal.Id);
                    var medicine = unitOfWork.Medicines.Get(selectedMedicine.Id);

                    animalId = animal.Id;
                    animal.AssignedMedicines.Remove(medicine);

                    Appointments = new ObservableCollection<Appointment>(unitOfWork.Appointments.GetAll());

                    unitOfWork.Save();
                }


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

            if(selectedMedicine != null)
                await Task.Run(() => AppendLog($"{selectedMedicine.Name} medicine has been unassigned from animal with Id:{animalId}."));
        }


        /// <summary>
        /// Enable/disable auto scroll in logs window.
        /// </summary>
        /// <returns></returns>
        public async Task AutoScrollLogsAsync()
        {
            AutoScrollLogs = !AutoScrollLogs;
        }


        /// <summary>
        /// Shuts down an application.
        /// </summary>
        /// <returns></returns>
        private async Task ExitTheAppAsync()
        {
            App.Current.Shutdown();
        }


        /// <summary>
        /// Right mouse click handling. 
        /// </summary>
        /// <returns></returns>
        private async Task RightMouseClickAsync()
        {
            await SetNullToSelectableProperties();
        }

        #endregion

        #region Event callbacks

        /// <summary>
        /// Callback raised on status changed event.
        /// </summary>
        /// <returns></returns>
        public async Task OnRaiseStatusOfAppointmentChangedEvent()
        {
            if (SelectedAppointment == null)
                return;

            using (var uow = new UnitOfWork())
            {
                var selectedAppointment = uow.Appointments.Get(SelectedAppointment.Id);

                selectedAppointment.StateOfVisit = SelectedStatusOfVisit;

                Appointments = new ObservableCollection<Appointment>(uow.Appointments.GetAll());

                uow.Save();
            }


            if(SelectedAppointment != null)
                await Task.Run(() => AppendLog($"Status of appointment with Id:{SelectedAppointment.Id} has been changed."));
        }


        /// <summary>
        /// Callback raised change type of filtering the appointment event.
        /// </summary>
        /// <param name="filterSelection"></param>
        /// <returns></returns>
        private async Task OnRaiseFilterAppointmentChangedEvent(string filterSelection)
        {
            switch (filterSelection)
            {
                case _showAllAppointments:
                    FilteredAppointments = Appointments;
                    break;
                case _showPastAppointments:
                    FilteredAppointments = new ObservableCollection<Appointment>(Appointments.Where(app => app.Date < DateTime.Now.Date));
                    break;
                case _showUpcomingAppointments:
                    FilteredAppointments = new ObservableCollection<Appointment>(Appointments.Where(app => app.Date >= DateTime.Now.Date));
                    break;
                default:
                    break;
            }

            RaisePropertyChanged(() => FilteredAppointments);
        }
        #endregion

        #region Method and tasks definitions
        /// <summary>
        /// Appends log to log window
        /// </summary>
        /// <param name="log"></param>
        public void AppendLog(string log)
        {
            if (log == null)
                return;

            DateTime dateTime = DateTime.Now;
            var cultureInfo = new CultureInfo("en-US");
            LogsToShow = Logs.AppendFormat("> {0} :: {1} \n", dateTime.ToString(cultureInfo), log).ToString();

        }


        /// <summary>
        /// Nulls all selectable properties.
        /// </summary>
        /// <returns></returns>
        private async Task SetNullToSelectableProperties()
        {
            SelectedAnimal = null;
            SelectedAnimalBasicInfo = null;
            SelectedAnimalInAppointmentTab = null;
            SelectedAppointment = null;
            SelectedMedicine = null;
            SelectedCustomer = null;
            SelectedCustomerInAppointmentTab = null;
        }


        /// <summary>
        /// Sets appointment form values to defualt.
        /// </summary>
        /// <returns></returns>
        private async Task ResetAppointmentFormValues()
        {
            CustomerToAddFirstName = null;
            CustomerToAddLastName = null;
            CustomerToAddCity = null;
            CustomerToAddPostalCode = null;
            CustomerToAddStreet = null;
            CustomerToAddHouseNumber = 1;
            CustomerToAddContact = null;

            AnimalToAddName = null;
            AnimalToAddGender = Gender.Female;

            SelectedCustomerInAppointmentTab = null;
            SelectedAnimalInAppointmentTab = null;

            AppointmentDate = DateTime.Now;
            AppointmentDescription = null;
            AppointmentPurposeOfVisit = PurposeOfVisit.FirstVisit;
        }


        /// <summary>
        /// Checks if appointment could be made.
        /// </summary>
        /// <returns></returns>
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
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Logs = new StringBuilder("");

            // Add callback to the event handlers
            RaiseStatusOfAppointmentChangedEvent += OnRaiseStatusOfAppointmentChangedEvent;
            RaiseFilterAppointmentChangedEvent += OnRaiseFilterAppointmentChangedEvent;

            using (var uow = new UnitOfWork())
            {
                Appointments = new ObservableCollection<Appointment>(uow.Appointments.GetAll());
            }

            AppendLog("App started.");
        }
        #endregion

    }

}
