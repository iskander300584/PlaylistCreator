using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;


namespace PlaylistMaker.Converters
{
    public class GridViewColumnWidth_Converter : IMultiValueConverter
    {
        private static readonly System.Reflection.PropertyInfo s_piDesiredWidth;

        static GridViewColumnWidth_Converter()
        {
            var pi = typeof(GridViewColumn).GetProperty("DesiredWidth",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //Debug.Assert(pi != null);
            s_piDesiredWidth = pi;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string param && param == "*" && values.Length == 3 && values[0] is double actualWidth && actualWidth > 0d)
            {
                if (values[1] is GridView gridView && values[2] is GridViewColumn column && s_piDesiredWidth != null)
                {
                    double w = 0d;
                    foreach (var col in gridView.Columns)
                    {
                        if (col == column)
                            continue;
                        w += col.ActualWidth > 0 ? col.ActualWidth : (double)s_piDesiredWidth.GetValue(col);
                    }
                    double desiredWidth = actualWidth - w;
                    return desiredWidth > 100 ? (desiredWidth - 25) /*scrollbar width*/ : double.NaN;
                }
            }
            return double.NaN;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
