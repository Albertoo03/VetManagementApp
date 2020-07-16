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


    public class Animal
    {
        public Animal()
        {
            this.Appointments = new HashSet<Appointment>();
            this.AssignedMedicines = new HashSet<Medicine>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public virtual Customer Owner { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<Medicine> AssignedMedicines { get; set; }

        public bool IsCurrentlyBeingTreated { get; set; }

        public virtual AnimalBasicInfo SpeciesInfo { get; set; }


        public override string ToString()
        {
            return Name + ", " + SpeciesInfo.Species;
        }
    }
    

}
