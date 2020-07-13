using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VetManagementApp.Helpers
{
    public static class HelpfulUtilities
    {
        public enum AnimalGroup
        {
            Mammal,
            Bird,
            Reptile,
            Amphibian,
            Fish,
            Arthropod,
        };

        public enum AnimalSpecies
        {
            Dog,
            Cat,
            Cow,
            Horse,
            Frog,
            Fox,

        }

        public enum Gender
        {
            Female,
            Male
        };

        public enum PurposeOfVisit
        {
            FirstVisit,
            ControlVisit,
            Consultation,
            Treatment,
        };



        public enum DosageFrequency
        {
            OnceADay,
            OnceAWeek,
            TwiceADay,
            TwiceAWeek,
            Every6Hours,
            Every4Hours,
        };

        public enum PurposeOfUseOfMedicine
        {

        }

        public struct Dosage
        {
            public float DoseInMiligrams { get; set; }
            //public en
        }



        /// <summary>
        /// Checks if window of the name passed as an argument has not already been opened.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
    }
}
