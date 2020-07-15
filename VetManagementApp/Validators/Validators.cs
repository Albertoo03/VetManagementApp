using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VetManagementApp.Validators
{
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
}
