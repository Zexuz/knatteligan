using System;
using System.Globalization;
using System.Windows.Data;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF.Converters
{
    class LeagueNameToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value.GetType() != typeof(LeagueName))
                throw new Exception($"Cannot convert from type {value.GetType()}");

            var leagueName = (LeagueName)value;
            return leagueName.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.GetType() != typeof(string))
                throw new Exception($"Connot convert from type {value.GetType()}");

            var leagueName = new LeagueName(value.ToString());
            return leagueName;
        }
    }
}
