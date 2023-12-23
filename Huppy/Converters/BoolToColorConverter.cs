using System;
using System.Globalization;

using Avalonia.Media;
using Avalonia.Data.Converters;

namespace Huppy.Converters
{
public class BoolToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isChecked && isChecked)
        {
            return Brush.Parse("#00A550");
        }
        else
        {
            return Brush.Parse("#FF9966");
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}
