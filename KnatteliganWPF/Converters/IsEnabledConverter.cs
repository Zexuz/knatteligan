using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

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
                if (val.GetType() == typeof(string))
                {
                    var boolVal = !string.IsNullOrEmpty(val.ToString());
                    retValue = retValue && boolVal;
                }
                else if (val.GetType() == typeof(ValidationError))
                {
                    retValue = false;
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
