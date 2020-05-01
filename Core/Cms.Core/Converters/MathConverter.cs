using Cms.Core.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Cms.Core.Converters
{
    /// <summary>
    ///     Performs arithmetic operations on a double (or double-convertible type) and returns the calculated value.
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public sealed class MathConverter : IValueConverter
    {
        #region  Properties

        /// <summary>
        ///     Indicates the mathematical operation that must be performed on the value being converted.
        /// </summary>
        public ArithmeticOperation Operation { get; set; }

        /// <summary>
        ///     The argument that the value being converted will be mathematically adjusted with.
        /// </summary>
        public double OperationalArgument { get; set; }

        #endregion

        #region  Interface Implementations

        /// <summary>
        ///     Converts a double value to a new double value based on the mathematical operation (<see cref="Operation"/>) and operation argument (<see cref="OperationalArgument"/>)
        ///     specified. Order of operations is left to right, <see cref="value"/>, <see cref="Operation"/>, <see cref="OperationalArgument"/>.
        /// </summary>
        /// <inheritdoc cref="IValueConverter.Convert"/>
        /// <example>
        ///     Value being converted: 10 <see cref="Operation"/>: <see cref="ArithmeticOperation.Addition"/> <see cref="OperationalArgument"/> : 5 Result: 10 + 5 = 15
        ///     Value being converted: 10 <see cref="Operation"/>: <see cref="ArithmeticOperation.Subtraction"/> <see cref="OperationalArgument"/> : 5 Result: 10 - 5 = 5
        ///     Value being converted: 10 <see cref="Operation"/>: <see cref="ArithmeticOperation.Multiplication"/> <see cref="OperationalArgument"/> : 5 Result: 10 * 5 = 50
        ///     Value being converted: 10 <see cref="Operation"/>: <see cref="ArithmeticOperation.Division"/> <see cref="OperationalArgument"/> : 5 Result: 10 / 5 = 2
        /// </example>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!double.TryParse(value.ToString(), out var doubleResult))
                throw new ArgumentException(
                    $@"The value being converted, {value}, is not convertible to a double type.", nameof(value));

            switch (Operation)
            {
                case ArithmeticOperation.Addition:
                    return doubleResult + OperationalArgument;

                case ArithmeticOperation.Subtraction:
                    return doubleResult - OperationalArgument;

                case ArithmeticOperation.Multiplication:
                    return doubleResult * OperationalArgument;

                case ArithmeticOperation.Division:
                    if (OperationalArgument == 0)
                        throw new DivideByZeroException(
                            $@"Mathematical conversion for division for an {nameof(OperationalArgument)} of zero cannot be performed.");

                    return doubleResult / OperationalArgument;

                default:
                    throw new NotSupportedException(
                        $@"Mathematical conversion is not currently supported for the operation, {Operation.ToString()}.");
            }
        }

        /// <summary>
        ///     Converts a double value to a new double value based on the inverse of the mathematical operation (<see cref="Operation"/>) and operation argument (<see cref="OperationalArgument"/>)
        ///     specified. Order of operations is left to right, <see cref="value"/>, <see cref="Operation"/>, <see cref="OperationalArgument"/>.
        /// </summary>
        /// <inheritdoc cref="IValueConverter.ConvertBack"/>
        /// <example>
        ///     Value being converted: 10 <see cref="Operation"/>: <see cref="ArithmeticOperation.Addition"/> <see cref="OperationalArgument"/> : 5 Result: 10 - 5 = 5
        ///     Value being converted: 10 <see cref="Operation"/>: <see cref="ArithmeticOperation.Subtraction"/> <see cref="OperationalArgument"/> : 5 Result: 10 + 5 = 15
        ///     Value being converted: 10 <see cref="Operation"/>: <see cref="ArithmeticOperation.Multiplication"/> <see cref="OperationalArgument"/> : 5 Result: 10 / 5 = 2
        ///     Value being converted: 10 <see cref="Operation"/>: <see cref="ArithmeticOperation.Division"/> <see cref="OperationalArgument"/> : 5 Result: 10 * 5 = 50
        /// </example>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!double.TryParse(value.ToString(), out var doubleResult))
                throw new ArgumentException(
                    $@"The value being converted, {value}, is not convertible to a double type.", nameof(value));

            switch (Operation)
            {
                case ArithmeticOperation.Addition:
                    return doubleResult - OperationalArgument;

                case ArithmeticOperation.Subtraction:
                    return doubleResult + OperationalArgument;

                case ArithmeticOperation.Multiplication:
                    if (OperationalArgument == 0)
                        throw new DivideByZeroException(
                            $@"Mathematical backwards conversion for multiplication for an {nameof(OperationalArgument)} of zero cannot be performed.");

                    return doubleResult / OperationalArgument;

                case ArithmeticOperation.Division:
                    return doubleResult * OperationalArgument;

                default:
                    throw new NotSupportedException(
                        $@"Mathematical conversion is not currently supported for the operation, {Operation.ToString()}.");
            }
        }

        #endregion
    }
}
