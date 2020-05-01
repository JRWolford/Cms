using System;
using System.Globalization;
using System.Windows.Data;

namespace Cms.Core.Converters
{
    /// <summary>
    ///     An <see cref="IValueConverter" /> class that inverts a boolean value. Taking a true
    ///     value and making it false or a false value and making it true.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class BooleanInverter : IValueConverter
    {
        /// <summary>
        ///     Converts a boolean value to its opposite binary state.
        /// </summary>
        /// <returns>
        ///     True if the <paramref name="value"/> is false, false if the <paramref name="value"/>
        ///     is true.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     This exception is raised if the <paramref name="value"/> argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     This exception is raised if the <paramref name="value"/> argument is not of type
        ///     boolean.
        /// </exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException($@"The value to be converted was null.", nameof(value));

            if (!(value is bool booleanValue))
                throw new ArgumentException($@"The value to be converted is not a boolean type.", nameof(value));

            return !booleanValue;
        }

        /// <summary>
        ///     This method is not supported.
        /// </summary>
        /// <inheritdoc cref="IValueConverter.ConvertBack" />
        /// <exception cref="NotSupportedException">
        ///     This exception is always thrown as this method is not currently supported.
        /// </exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
