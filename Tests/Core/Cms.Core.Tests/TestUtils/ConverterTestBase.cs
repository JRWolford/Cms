using NUnit.Framework;
using System;
using System.Reflection;
using System.Windows.Data;

namespace Cms.Core.Tests.TestUtils
{
    /// <summary>
    ///     This test utility contains all tests and functionality that is common to all converter
    ///     classes being tested.
    /// </summary>
    /// <typeparam name="TConverterType">
    ///     The converter class being tested.
    /// </typeparam>
    /// <typeparam name="TSourceType">
    ///     The anticipated source type being converted.
    /// </typeparam>
    /// <typeparam name="TTargetType">
    ///     The anticipated target type being returned.
    /// </typeparam>
    public abstract class ConverterTestBase<TConverterType, TSourceType, TTargetType> where TConverterType : class, IValueConverter
    {
        /// <summary>
        ///     Asserts that the <see cref="TConverterType"/> class is marked with the 
        ///     <see cref="ValueConversionAttribute"/> attribute.
        /// </summary>
        [Test]
        public void ShouldBeMarkedWithValueConversionAttribute() =>
            Assert.That(typeof(TConverterType).IsMarkedWithAttribute(typeof(ValueConversionAttribute)));

        /// <summary>
        ///     Asserts that the converter class is sealed.
        /// </summary>
        [Test]
        public void ConverterClassShouldBeSealed() =>
            Assert.That(typeof(TConverterType).IsSealed);

        /// <summary>
        ///     Asserts that the <see cref="ValueConversionAttribute.SourceType" /> matches the specified
        ///     <see cref="TSourceType" /> type.
        /// </summary>
        [Test]
        public void ValueConversionSourceTypeShouldMatchTSourceType() =>
            Assert.AreEqual(typeof(TSourceType), ((ValueConversionAttribute)typeof(TConverterType)
                    .GetCustomAttribute(typeof(ValueConversionAttribute))).SourceType);

        /// <summary>
        ///     Asserts that the <see cref="ValueConversionAttribute.TargetType" /> matches the specified
        ///     <see cref="TTargetType" /> type.
        /// </summary>
        [Test]
        public void ValueConversionTargetTypeShouldMatchTTargetType() =>
            Assert.AreEqual(typeof(TTargetType), ((ValueConversionAttribute)typeof(TConverterType)
                    .GetCustomAttribute(typeof(ValueConversionAttribute))).TargetType);
    }
}
