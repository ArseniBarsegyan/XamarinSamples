﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace Templates.Converters
{
    /// <summary>
    /// Convert string to bool. If string is null or empty returns false.
    /// </summary>
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (string)value;
            return !string.IsNullOrWhiteSpace(val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
