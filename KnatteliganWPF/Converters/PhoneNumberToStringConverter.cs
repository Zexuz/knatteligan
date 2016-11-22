using System;
using System.Globalization;
using System.Windows.Data;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF.Converters
{
    public class PhoneNumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value.GetType() != typeof(PhoneNumber))
                throw new Exception($"Cannot convert from type {value.GetType()}");

            var phoneNumber = (PhoneNumber)value;
            return phoneNumber.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.GetType() != typeof(string))
                throw new Exception($"Connot convert from type {value.GetType()}");

            var phoneNumber = new PhoneNumber(value.ToString());
            return phoneNumber;
        }
    }
}
