﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyDiary.Converters
{
    /// <summary>
    /// Convert positive integer value to bool. Return true if value > 0.
    /// </summary>
    public class NumberToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int)value;
            if (val > 0)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}