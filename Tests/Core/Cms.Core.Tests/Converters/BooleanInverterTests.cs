using Cms.Core.Converters;
using Cms.Core.Tests.TestUtils;
using NUnit.Framework;
using System;
using System.Globalization;

namespace Cms.Core.Tests.Converters
{
    /// <summary>
    ///     This test fixture contains all tests for the <see cref="BooleanInverter"/> class.
    /// </summary>
    [TestFixture]
    public sealed class BooleanInverterTests : ConverterTestBase<BooleanInverter, bool, bool>
    {
        /// <summary>
        ///     Asserts that the <see cref="BooleanInverter.Convert(object, Type, object, CultureInfo)"/> method
        ///     properly inverts a boolean value.
        /// </summary>
        /// <param name="originalValue"></param>
        [TestCase(true)]
        [TestCase(false)]
        public void ConvertShouldInvertTheValueProvided(bool originalValue)
        {
            //Arrange
            bool convertedValue = originalValue;
            var converter = new BooleanInverter();

            //Act
            convertedValue = (bool)converter.Convert(originalValue, typeof(bool), null, CultureInfo.CurrentCulture);

            //Assert
            Assert.AreEqual(!originalValue, convertedValue);
        }

        /// <summary>
        ///     Asserts that the <see cref="BooleanInverter.ConvertBack(object, Type, object, CultureInfo)"/> method 
        ///     raises the <see cref="NotSupportedException"/> whenever it is called.
        /// </summary>
        [Test]
        public void ConvertBackShouldRaiseNotSupportedException()
        {
            //Arrange
            var converter = new BooleanInverter();

            //Assert
            Assert.Throws<NotSupportedException>(() => converter.ConvertBack(true, typeof(bool), null, CultureInfo.CurrentCulture));
        }

        /// <summary>
        ///     Asserts that the <see cref="BooleanInverter.Convert(object, Type, object, CultureInfo)"/> method raises an
        ///     <see cref="ArgumentNullException"/> whenever the value parameter is null.
        /// </summary>
        [Test]
        public void ConvertShouldRaiseArgumentNullExceptionWhenValueIsNull()
        {
            //Arrange
            var converter = new BooleanInverter();

            //Act
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, typeof(bool), null, CultureInfo.CurrentCulture));
        }

        /// <summary>
        ///     Asserts that the <see cref="BooleanInverter.Convert(object, Type, object, CultureInfo)"/> method raises an
        ///     <see cref="ArgumentException"/> whenever the value parameter is not of type of boolean.
        /// </summary>
        [Test]
        public void ConvertShouldRaiseArgumentExceptionWhenValueIsNotOfBooleanType()
        {
            //Arrange
            var converter = new BooleanInverter();

            //Act
            Assert.Throws<ArgumentException>(() => converter.Convert(6.23, typeof(bool), null, CultureInfo.CurrentCulture));
        }
    }
}
