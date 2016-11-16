using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using knatteligan.Domain.Entities;

namespace KnatteliganWPF.Converters
{
    class IsEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool retValue = true;

            foreach (var val in values)
            {
                if (val == null)
                    continue;
                if (val is string)
                {
                    var boolVal = !string.IsNullOrEmpty(val.ToString());
                    retValue = boolVal;
                }
                else if (val.GetType() == typeof(ValidationError))
                {
                    retValue = false;
                }
                
                else if (val is int)
                {
                    var count = (int) val;
                    retValue = count%2 == 0;
                }
                if (!retValue)
                    break;
            }
            return retValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
