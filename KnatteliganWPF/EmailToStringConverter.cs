using knatteligan.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KnatteliganWPF
{
    class EmailToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            if (value.GetType() == typeof(Email))
            {
                var email = (Email)value;
                return email.Value;
            }
            throw new Exception($"Cannot convert from type {value.GetType().ToString()}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.GetType() == typeof(string))
            {
                var email = new Email(value.ToString());
                return email;
            }
            throw new Exception($"Connot convert from type {value.GetType().ToString()}");
        }
    }
}
