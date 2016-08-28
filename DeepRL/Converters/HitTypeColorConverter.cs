using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using DeepRL.Helpers;

namespace DeepRL.Converters
{
    public class HitTypeColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hitType = value as HitType?;

            if (hitType.HasValue)
            {
                switch (hitType.Value)
                {
                    case HitType.Fruit:
                        return Brushes.OrangeRed;
                    case HitType.Poison:
                        return Brushes.LawnGreen;
                    default:
                        return Brushes.Black;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
