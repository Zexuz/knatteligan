using System;
using System.Globalization;
using System.Windows.Data;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF.Converters
{
    public class TeamNameToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value.GetType() != typeof(TeamName))
                throw new Exception($"Cannot convert from type {value.GetType()}");

            var teamName = (TeamName)value;
            return teamName.Value;
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
