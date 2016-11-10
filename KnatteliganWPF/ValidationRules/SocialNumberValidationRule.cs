using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KnatteliganWPF.ValidationRules
{
    class SocialNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {

            }
            catch (Exception)
            {
                return new ValidationResult(false, "Not a valid social number");
            }
        }
    }
}
