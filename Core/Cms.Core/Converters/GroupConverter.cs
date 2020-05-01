using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Cms.Core.Converters
{
    /// <summary>
    ///     Provides functionality for chaining multiple converters together to get a custom conversion.
    /// </summary>
    /// <remarks>
    ///     Each successive conversion's target type must be the same as the expected source value type.
    /// </remarks>
    public sealed class GroupConverter : List<IValueConverter>, IValueConverter
    {
        /// <summary>
        ///     Progresses through all of the <see cref="IValueConverter"/> objects in the list in chronological order and
        ///     converts the <see cref="value"/> and passes it's converted value to the next converter until all
        ///     conversions have been aggregated up the chain.
        /// </summary>
        /// <remarks>
        ///     This converter can be used to combine multiple <see cref="IValueConverter"/>s to create a custom
        ///     converter without having to actually create it. The example shows how you can combine a <see cref="BooleanInverter"/>
        ///     with the <see cref="BooleanToVisibilityConverter"/> to create a "BooleanToVisibilityInverter".
        /// </remarks>
        /// <example>
        ///
        ///     {UserControl.Resources}
        ///         {converters:GroupConverter x:Key="BooleanToVisibilityInverter"}
        ///             {converters:<see cref="BooleanInverter"/>/}
        ///             {BooleanToVisibilityConverter/}
        ///         {/converters:GroupConverter}
        ///     {/UserControl.Resources}
        /// 
        /// </example>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));

        /// <summary>
        ///     This method is not supported.
        /// </summary>
        /// <inheritdoc cref="IValueConverter.ConvertBack"/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
