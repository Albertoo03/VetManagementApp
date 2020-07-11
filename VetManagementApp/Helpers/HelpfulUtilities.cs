using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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
    }
}
