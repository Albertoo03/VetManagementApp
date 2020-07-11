using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VetManagementApp.Helpers.HelpfulUtilities;
using static VetManagementApp.Interfaces.Interfaces;

namespace VetManagementApp.Model
{
    public class AnimalBasicInfo
    {
        [Key]
        public string Species { get; set; }

        public AnimalGroup Group { get; set; }
        public virtual ObservableCollection<Medicine> AvailableMedicines { get; set; }
    }

    public class Animal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public virtual Customer Owner { get; set; }
        public virtual ObservableCollection<Appointment> Appointments { get; set; }

        public virtual ObservableCollection<Medicine> AssignedMedicines { get; set; }
        //public enum PrescribedMedication { get;set; }

        public bool IsCurrentlyBeingTreated { get; set; }

        public virtual AnimalBasicInfo SpeciesInfo { get; set; }
        //public virtual ObservableCollection<Medicine> GetListOfAvailableMedicines()
        //{
        //    throw new NotImplementedException();
        //}
    }

    //public class TreatedAnimal : AnimalBase
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public Gender Gender { get; set; }
    //    public virtual Customer Owner { get; set; }
    //    public virtual ObservableCollection<Appointment> Appointments { get; set; }

    //    //public enum PrescribedMedication { get;set; }

    //    public bool IsCurrentlyBeingTreated { get; set; }
    //    //public virtual ObservableCollection<Medicine> GetListOfAvailableMedicines()
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}
    

}
