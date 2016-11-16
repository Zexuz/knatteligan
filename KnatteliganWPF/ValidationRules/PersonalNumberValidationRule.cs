using knatteligan.Domain.ValueObjects;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace KnatteliganWPF.ValidationRules
{
    class PersonalNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //961107-1136
            var str = value.ToString();
            
            Console.WriteLine(value.ToString());
            try
            {

                var ayear = int.Parse(str.Substring(0, 2));
                var month = int.Parse(str.Substring(2, 2));
                var day = int.Parse(str.Substring(4, 2));
                var last4 = str.Substring(7, 4);


                var date = new DateTime(ayear, month, day);

                new PersonalNumber(date, last4);
                //throw new Exception();
                return new ValidationResult(true, "");
                //new (value.ToString());
                //return new ValidationResult(true, "");
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Not a valid social number");
            }
        }
    }
}
