using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace TaskTurner.Models
{
    public class ImportanceToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TaskImportance))
                return Brushes.Transparent;

            var status = (TaskImportance)value;

            switch (status)
            {
                case TaskImportance.Low:
                    return Application.Current.FindResource("Low") as Brush;
                case TaskImportance.Medium:
                    return Application.Current.FindResource("Medium") as Brush;
                case TaskImportance.High:
                    return Application.Current.FindResource("High") as Brush;
                case TaskImportance.Critical:
                    return Application.Current.FindResource("Critical") as Brush;
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
