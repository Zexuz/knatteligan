using System;
using System.Globalization;
using System.Windows.Data;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF.Converters
{
    public class PersonNameToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value.GetType() != typeof(PersonName))
                throw new Exception($"Cannot convert from type {value.GetType()}");

            var personName = (PersonName) value;
            return personName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.GetType() != typeof(string))
                throw new Exception($"Cannot convert from type {value.GetType()}");

            var personName = new PersonName(value.ToString());
            return personName;
        }
    }
}
