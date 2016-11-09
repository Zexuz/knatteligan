using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF
{
    public class PersonNameToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value.GetType() != typeof(PersonName))
                throw new Exception($"Cannot convert from type {value.GetType()}");

            var personName = (PersonName)value;
            return personName.FirstName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.GetType() != typeof(string))
                throw new Exception($"Connot convert from type {value.GetType()}");

            var teamName = new TeamName(value.ToString());
            return teamName;
        }
    }
}
