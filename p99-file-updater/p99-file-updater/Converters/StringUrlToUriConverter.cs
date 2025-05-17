using System;
using Windows.UI.Xaml.Data;

namespace p99FileUpdater.Converters
{
    internal class StringUrlToUriConverter : IValueConverter
    {
        public object Convert(object convertValue, Type targetType, object parameter, string language)
        {   if(convertValue != null)
                return new Uri((String)convertValue).OriginalString;
            return null;
        }

        public object ConvertBack(object convertBackValue, Type targetType, object parameter, string language)
        {
            return new Uri((String)convertBackValue);
        }
    }
}
