using System;
using System.Windows;
using System.Windows.Data;

namespace Efficient_developer_helper
{
    /// <summary>
    /// Converter that returns a visibility depending on the content of a string
    /// </summary>
    public class EmptyStringToVisibilityConverter : IValueConverter
    {

        /// <summary>
        /// Converts a string which is null or empty to Visibility.Collapsed. Not empty will be Visibiliy.Visible. 
        /// Set ConverterParameter to true to invert the visibility converter. 
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var str = value as string;
            var b = string.IsNullOrEmpty(str);

            if (parameter != null && bool.Parse((string) parameter))
                b = !b;

            return b ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("Two way conversion is not supported.");
        }
    }
}
