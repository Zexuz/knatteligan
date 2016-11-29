using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KnatteliganWPF.Converters
{
    class IsSelectedMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var retValue = true;
            foreach (var value in values)
            {
                if (value is int)
                {
                    var count = (int) value;
                    retValue = count == 1;
                }
            }
            return retValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
