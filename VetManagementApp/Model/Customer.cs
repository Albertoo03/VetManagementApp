using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace VetManagementApp.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(15)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get => FirstName + " " + LastName;
        }

        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string PostalCode { get; set; }

        public string Address
        {
            get => Street + " " + HouseNumber + Environment.NewLine + PostalCode + " " + City;
        }

        public string Contact { get; set; }



        public virtual ICollection<Animal> OwnedAnimals { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public override string ToString()
        {
            return FullName + Environment.NewLine + Address;
        }

    }

}
