using System;
using System.Text;
using Windows.UI.Xaml.Data;

namespace p99FileUpdater.Converters
{
    internal class ChecksumByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null)
                return String.Empty;
            StringBuilder sb = new StringBuilder();
            byte[] checksumToConvert = (byte[])value;
            foreach (byte b in checksumToConvert)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((string)value == String.Empty)
                return null;
            char[] str = ((String)value).ToCharArray();
            byte[] checksum = new byte[32];
            for (int i = 0; i < 32; i++)
        {
                String s = String.Concat(str[i*2], str[(i*2) + 1]);
                byte b = System.Convert.ToByte(s, 16);
                checksum[i] = b;
        }
            return checksum;
        }
    }
}
