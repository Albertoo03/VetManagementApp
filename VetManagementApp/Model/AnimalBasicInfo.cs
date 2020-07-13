using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VetManagementApp.Helpers.HelpfulUtilities;

namespace VetManagementApp.Model
{
    public class AnimalBasicInfo
    {
        [Key]
        public string Species { get; set; }

        public AnimalGroup Group { get; set; }
        public virtual ICollection<Medicine> AvailableMedicines { get; set; }
    }
}
