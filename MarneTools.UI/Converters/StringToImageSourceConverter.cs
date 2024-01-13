namespace MarneTools.UI.Converters;

public class StringToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var path = value as string;
        if (string.IsNullOrWhiteSpace(path))
            return null;

        return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}