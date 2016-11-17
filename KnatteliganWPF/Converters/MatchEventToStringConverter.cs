using knatteligan.Domain.ValueObjects;
using System;
using System.Globalization;
using System.Windows.Data;
using knatteligan.Domain.Entities;

namespace KnatteliganWPF.Converters
{
    class MatchEventToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            var goal = value as Goal;
            var assist = value as Assist;
            var redCard = value as RedCard;
            var yellowCard = value as YellowCard;

            if (goal != null)
            {
                return goal.ToString();
            }
            if (assist != null)
            {
                return assist.ToString();
            }
            if (redCard != null)
            {
                return redCard.ToString();
            }
            if (yellowCard != null)
            {
                return yellowCard.ToString();
            }

            return "";
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