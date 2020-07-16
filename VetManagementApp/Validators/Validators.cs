using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VetManagementApp.Validators
{

    /// <summary>
    /// House number validation.
    /// </summary>
    public class HouseNumberValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int houseNumber;
            bool parsingSucceeded = Int32.TryParse(value.ToString(), out houseNumber);

            if(!parsingSucceeded)
                return new System.Windows.Controls.ValidationResult(false, "House number must be a number value.");

            if (houseNumber < 1)
                return new System.Windows.Controls.ValidationResult(false, "House number must be value greater than 0.");

            return System.Windows.Controls.ValidationResult.ValidResult;
        }
    }


    /// <summary>
    /// Postal code validation.
    /// </summary>
    public class PostalCodeValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string postalCode = value.ToString();

            if(!Regex.IsMatch(postalCode, "^\\d{2}[- ]{0,1}\\d{3}$"))
                return new System.Windows.Controls.ValidationResult(false, "Postal code must have a pattern XX-XXX.");


            return System.Windows.Controls.ValidationResult.ValidResult;
        }
    }
}
