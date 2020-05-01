using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cms.Core.Converters
{
    /// <summary>
    ///     Converts any numeric value into a margin <see cref="Thickness"/>.
    /// </summary>
    /// <remarks>
    ///     Functionality for converting only one, more, or all orthogonal direction margins is provided
    ///     through the properties <see cref="TopMultiplier"/>, <see cref="BottomMultiplier"/>, <see cref="LeftMultiplier"/>,
    ///     <see cref="RightMultiplier"/>.
    ///
    ///     The default values for the orthogonal multipliers is zero (0). If you do not set the value for an orthogonal
    ///     multiplier then it's resulting <see cref="Thickness"/> property will be zero (0).
    /// </remarks>
    [ValueConversion(typeof(double), typeof(Thickness))]
    public sealed class MarginConverter : IValueConverter
    {
        #region  Properties

        /// <summary>
        ///     A multiplication factor that will be applied to the double value being converted for the
        ///     <see cref="Thickness.Bottom" /> margin.
        /// </summary>
        public double BottomMultiplier { get; set; }

        /// <summary>
        ///     A multiplication factor that will be applied to the double value being converted for the
        ///     <see cref="Thickness.Left" /> margin.
        /// </summary>
        public double LeftMultiplier { get; set; }

        /// <summary>
        ///     A multiplication factor that will be applied to the double value being converted for the
        ///     <see cref="Thickness.Right" /> margin.
        /// </summary>
        public double RightMultiplier { get; set; }

        /// <summary>
        ///     A multiplication factor that will be applied to the double value being converted for the
        ///     <see cref="Thickness.Top" /> margin.
        /// </summary>
        public double TopMultiplier { get; set; }

        #endregion

        #region Methods

        /// <remarks>
        ///     Any value types that can be converted into a <see cref="double" /> can use this
        ///     converter. If a <see cref="value" /> cannot be converted into a <see cref="double" />
        ///     a <see cref="Thickness" /> object with top, bottom, left, and right values of zero
        ///     will be returned.
        /// </remarks>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new Thickness(0);

            if (!double.TryParse(value.ToString(), out var d))
                return new Thickness(0);

            return new Thickness(LeftMultiplier * d, TopMultiplier * d, RightMultiplier * d, BottomMultiplier * d);
        }

        /// <summary>
        ///     This method is not currently supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();

        #endregion
    }
}
