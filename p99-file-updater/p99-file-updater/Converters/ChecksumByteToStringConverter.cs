using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace p99FileUpdater.Converters
{
    internal class ChecksumByteToStringConverter : StringConverter
    {
        public new bool CanConvertTo(Type destinationType)
        {
            if (destinationType.Equals(typeof(byte[])))
                return true;
            else
                return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Equals(typeof(byte[])))
                return true;
            else
                return false;
        }

        public new bool CanConvertFrom(Type sourceType)
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType Null");
            else if (sourceType.Equals(typeof(String)))
                return true;
            else
                return false;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {

            if (sourceType.Equals(typeof(String)))
                return true;
            else
                return false;
        }

        public new object ConvertTo(object value, Type destinationType) => ConvertTo((byte[])value);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return String.Format(culture, ConvertTo((byte[])value), String.Empty);
        }

        private string ConvertTo(byte[] checksumToConvert)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in checksumToConvert)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public new object ConvertFrom(object value)
        {
            return ConvertFrom((string)value);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return ConvertFrom((string)value);
        }

        private byte[] ConvertFrom(String stringValue)
        {
            byte[] convertedFromValue = new byte[stringValue.Length / 2];
            for (int i = 0; i < stringValue.Length; i += 2)
                convertedFromValue[i] = byte.Parse(stringValue.Substring(i, 2));
            return convertedFromValue;
        }
    }
}
