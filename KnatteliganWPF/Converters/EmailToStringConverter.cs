using knatteligan.Domain.ValueObjects;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KnatteliganWPF.Converters
{
    class EmailToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            if (value.GetType() != typeof(Email))
                throw new Exception($"Cannot convert from type {value.GetType()}");

            var email = (Email)value;
            return email.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.GetType() != typeof(string))
                throw new Exception($"Connot convert from type {value.GetType()}");

            var email = new Email(value.ToString());
            return email;
        }
    }
}
