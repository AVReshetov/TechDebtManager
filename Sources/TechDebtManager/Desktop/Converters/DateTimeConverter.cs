using System;
using System.Globalization;
using System.Windows.Data;

namespace Desktop.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object      value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
        {
            return "[" + ((DateTime) value).ToString("dd.MM.yyyy HH:mm") + "] ";
        }

        /// <inheritdoc />
        public object ConvertBack(object      value,
                                  Type        targetType,
                                  object      parameter,
                                  CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
