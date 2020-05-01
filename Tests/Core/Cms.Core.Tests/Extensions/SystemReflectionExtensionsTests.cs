using NUnit.Framework;
using System;
using System.Reflection;

namespace Cms.Core.Tests.Extensions
{
    /// <summary>
    ///     This test fixture contains all tests for the <see cref="SystemReflectionExtensions"/> class.
    /// </summary>
    /// <remarks>
    ///     This test fixture makes use of the stub classes kept in the <see cref="SystemExtensionsTests"/>
    ///     class.
    /// </remarks>
    [TestFixture]
    public sealed class SystemReflectionExtensionsTests
    {
        #region Private Fields

        private readonly PropertyInfo _baseAttributePropertyInfo;

        private readonly PropertyInfo _inheritedAttributePropertyInfo;

        private readonly PropertyInfo _basePropertyInfo;

        private readonly PropertyInfo _inheritedPropertyInfo;

        #endregion

        #region Constructors

        public SystemReflectionExtensionsTests()
        {
            _baseAttributePropertyInfo = typeof(BaseClass).GetProperty(nameof(BaseClass.PropertyMarkedWithAttribute));
            _inheritedAttributePropertyInfo = typeof(ParentClass).GetProperty(nameof(ParentClass.PropertyMarkedWithAttribute));

            _basePropertyInfo = typeof(BaseClass).GetProperty(nameof(BaseClass.PropertyNotMarkedWithAttribute));
            _inheritedPropertyInfo = typeof(ParentClass).GetProperty(nameof(ParentClass.PropertyNotMarkedWithAttribute));
        }

        #endregion

        #region Tests

        /// <summary>
        ///     Asserts that the IsMarkedWithAttribute method raises an <see cref="ArgumentNullException"/>
        ///     when the attributeType parameter is null.
        /// </summary>
        [Test]
        public void IsMarkedWithAttributeShouldRaiseArgumentNullExceptionWhenAttributeTypeIsNull()
        {
            var argNullException =
                Assert.Throws<ArgumentNullException>(() => _baseAttributePropertyInfo.IsMarkedWithAttribute(null));

            Assert.AreEqual("attributeType", argNullException.ParamName);
        }

        /// <summary>
        ///     Asserts that the IsMarkedWithAttribute method raises an <see cref="ArgumentException"/>
        ///     when the attributeType parameter is not a type of attribute.
        /// </summary>
        [Test]
        public void IsMarkedWithAttributeShouldRaiseArgumentExceptionWhenAttributeTypeIsNotTypeOfAttribute()
        {
            var argException =
                Assert.Throws<ArgumentException>(() => _baseAttributePropertyInfo.IsMarkedWithAttribute(typeof(ParentClass)));

            Assert.AreEqual("attributeType", argException.ParamName);
        }

        /// <summary>
        ///     Asserts that the IsMarkedWithAttribute method returns true when the property is marked with the
        ///     correct attribute.
        /// </summary>
        [Test]
        public void IsMarkedWithAttributeShouldReturnTrueWhenPropertyIsMarkedWithAttribute() =>
            Assert.IsTrue(_baseAttributePropertyInfo.IsMarkedWithAttribute(typeof(CustomTestAttribute)));

        /// <summary>
        ///     Asserts that the IsMarkedWithAttribute method returns false when the property is not marked with
        ///     the correct attribute.
        /// </summary>
        [Test]
        public void IsMarkedWithAttributeShouldReturnFalseWhenPropertyIsNotMarkedWithAttribute() =>
            Assert.IsFalse(_basePropertyInfo.IsMarkedWithAttribute(typeof(CustomTestAttribute)));

        /// <summary>
        ///     Asserts that the IsMarkedWithAttribute method returns true when the inherited property is marked 
        ///     with the correct attribute.
        /// </summary>
        [Test]
        public void IsMarkedWithAttributeShouldReturnTrueWhenPropertyIsMarkedWithInheritedAttribute() =>
            Assert.IsTrue(_inheritedAttributePropertyInfo.IsMarkedWithAttribute(typeof(CustomTestAttribute), true));

        /// <summary>
        ///     Asserts that the IsMarkedWithAttribute method returns false when the inherited property is not 
        ///     marked with the correct attribute.
        /// </summary>
        [Test]
        public void IsMarkedWithAttributeShouldReturnFalseWhenPropertyIsNotMarkedWithInheritedAttribute() =>
            Assert.IsFalse(_inheritedPropertyInfo.IsMarkedWithAttribute(typeof(CustomTestAttribute), true));

        /// <summary>
        ///     Asserts that the IsMarkedWithAttribute method returns false when an inherited property is marked
        ///     with the attribute in the base class but is not marked with the attribute in the parent class and
        ///     the parameter, isInherited, is set to false.
        /// </summary>
        [Test]
        public void IsMarkedWithAttributeShouldReturnFalseWhenInheritedPropertyIsNotMarkedWithAttribute() =>
            Assert.IsFalse(_inheritedAttributePropertyInfo.IsMarkedWithAttribute(typeof(CustomTestAttribute), false));

        #endregion
    }
}
