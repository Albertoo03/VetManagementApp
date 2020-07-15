using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VetManagementApp.Model;
using static VetManagementApp.Helpers.HelpfulUtilities;

namespace VetManagementApp.Converters
{

    public class SelectedCustomerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return (Customer)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return (Customer)value;
        }
    }


    public class IsObjectNullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null) ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class GenderSelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isMaleChecked = (bool)value;

            return (isMaleChecked == true) ? Gender.Male : Gender.Female;
        }
    }

    //public class MakeAppointmentConditionsMultiConverter : IMultiValueConverter
    //{
    //    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        bool conditionsMet = true;

    //        var appointmentCustomer = (Customer)values[0];
    //        var appointmentAnimal = (Animal)values[1];
    //        var appointmentDate = (DateTime)values[2];
    //        var appointmentDescription = (string)values[3];

    //        if (appointmentCustomer == null || appointmentAnimal == null || appointmentDate == null || appointmentDescription == null)
    //            conditionsMet = false;
            

    //        return conditionsMet;
    //    }

    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
