using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetManagementApp.Model
{
    public class Medicine
    {
        public Medicine()
        {

        }

        public Medicine(string name, string manufacturer, string dose, string targetAnimal)
        {
            this.Name = name;
            this.Manufacturer = manufacturer;
            this.Dose = dose;
            this.TargetAnimal = targetAnimal;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Dose { get; set; }
        public string TargetAnimal { get; set; }
        public virtual ICollection<AnimalBasicInfo> AssignedAnimals { get; set; }

    }
}
