using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VetManagementApp.Model;
using static VetManagementApp.Helpers.HelpfulUtilities;

namespace VetManagementApp.Converters
{
    // Multi converters

    public class IsAssignMedicinesEnabledMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var isAnyAppointmentSelected = (bool)values[0];
            StateOfVisit selectedStateOfVisit;

            if (values[1] == DependencyProperty.UnsetValue)
                selectedStateOfVisit = StateOfVisit.WaitingForVisit;
            else
            {
                selectedStateOfVisit = (StateOfVisit)values[1];
            }
            

            return (isAnyAppointmentSelected && selectedStateOfVisit == StateOfVisit.VisitCompleted) ? true : false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[] { value, value };
        }
    }



    // Converters
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

}
