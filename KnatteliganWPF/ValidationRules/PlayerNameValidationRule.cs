using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KnatteliganWPF.ValidationRules
{
    class PlayerNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isMatch = Regex.IsMatch(value.ToString(),
                "^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð-]{2,30}$");
            if (isMatch)
            {
                return new ValidationResult(isMatch, null);
            }
            else
            {
                return new ValidationResult(isMatch, "Invalid name");
            }
        }
    }
}
