using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace KinectHandTracking.Helpers
{
    
    [ValueConversion(typeof(ObservableCollection<double>), typeof(String))]
    public class AnimationValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<double> l = value as ObservableCollection<double>;
            if (l == null)
                return DependencyProperty.UnsetValue;
            int i = int.Parse(parameter.ToString());

            return l[i].ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
