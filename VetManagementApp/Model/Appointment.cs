using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VetManagementApp.Helpers.HelpfulUtilities;

namespace VetManagementApp.Model
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public virtual Customer AppointedCustomer { get; set; }
        public virtual Animal AppointedAnimal { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public PurposeOfVisit PurposeOfVisit { get; set; }

    }
}
