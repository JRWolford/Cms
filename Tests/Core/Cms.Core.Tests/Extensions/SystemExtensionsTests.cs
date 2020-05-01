using NUnit.Framework;
using System;

namespace Cms.Core.Tests.Extensions
{
    /// <summary>
    ///     This test fixture contains all tests for the <see cref="SystemExtensions"/> class.
    /// </summary>
    [TestFixture]
    public sealed class SystemExtensionsTests
    {
        /// <summary>
        ///     Asserts that a null attribute type parameter throws the <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test]
        public void NullAttributeTypeShouldRaiseArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => typeof(BaseClass).IsMarkedWithAttribute(null));

        /// <summary>
        ///     Asserts that the attribute type parameter throws an <see cref="ArgumentException"/> when
        ///     it is not an attribute.
        /// </summary>
        [Test]
        public void InvalidAttributeTypeShouldRaiseArgumentException() =>
            Assert.Throws<ArgumentException>(() => typeof(ParentClass).IsMarkedWithAttribute(typeof(BaseClass)));

        /// <summary>
        ///     Asserts that a false value is returned when the attribute is on an inherited class and not
        ///     the parent class when the isInherited property is set to false.
        /// </summary>
        [Test]
        public void InheritedAttributesShouldReturnFalseWhenIsInheritedIsFalse() =>
            Assert.IsFalse(typeof(ParentClass).IsMarkedWithAttribute(typeof(CustomTestAttribute)));

        /// <summary>
        ///     Asserts that a true value is returned when the attribute is on an inherited class and not
        ///     the parent class when the isInherited property is set to true.
        /// </summary>
        [Test]
        public void InheritedAttributesShouldReturnTrueWhenIsInheritedIsTrue() =>
            Assert.IsTrue(typeof(ParentClass).IsMarkedWithAttribute(typeof(CustomTestAttribute), true));

        /// <summary>
        ///     Asserts that a class type that is marked with the expected attribute type returns true.
        /// </summary>
        [Test]
        public void ShouldReturnTrueWhenClassIsMarkedWithExpectedAttributeType() =>
            Assert.IsTrue(typeof(BaseClass).IsMarkedWithAttribute(typeof(CustomTestAttribute)));

        /// <summary>
        ///     Asserts that the method raises an <see cref="ArgumentException"/> when the classType parameter
        ///     is not a class.
        /// </summary>
        [Test]
        public void IsImplementingInterfaceShouldRaisArgumentExceptionWhenClassTypeParameterIsNotClass()
        {
            var argException = Assert.Throws<ArgumentException>(() => typeof(ITestInterface).IsImplementingInterface(typeof(ITestInterface)));
            Assert.AreEqual("classType", argException.ParamName);
        }

        /// <summary>
        ///     Asserts that the IsImplementingInterface method raises an <see cref="ArgumentNullException"/> 
        ///     when the interfaceType parameter is null.
        /// </summary>
        [Test]
        public void IsImplementingInterfaceShouldRaiseArgumentNullExceptionWhenInterfaceTypeParameterIsNull()
        {
            var argNullException = Assert.Throws<ArgumentNullException>(() => typeof(BaseClass).IsImplementingInterface(null));
            Assert.AreEqual("interfaceType", argNullException.ParamName);
        }

        /// <summary>
        ///     Asserts that the IsImplementingInterface method raises an <see cref="ArgumentException"/> when the
        ///     interfaceType parameter is not a type of interface.
        /// </summary>
        [Test]
        public void IsImplementInterfaceShouldRaiseArgumentExceptionWhenInterfaceTypeParameterIsNotInterfaceType()
        {
            var argException = Assert.Throws<ArgumentException>(() => typeof(BaseClass).IsImplementingInterface(typeof(ParentClass)));
            Assert.AreEqual("interfaceType", argException.ParamName);
        }

        /// <summary>
        ///     Asserts that the IsImplementingInterface method returns true when the class
        ///     implements the specified interface type.
        /// </summary>
        [Test]
        public void IsImplementingInterfaceShouldReturnTrueWhenClassImplementsInterfaceType() =>
            Assert.IsTrue(typeof(BaseClass).IsImplementingInterface(typeof(ITestInterface)));

        /// <summary>
        ///     Asserts that the IsImplementingInterface method returns false when the class
        ///     does not implement the specified interface type.
        /// </summary>
        [Test]
        public void IsImplementingInterfaceShouldReturnFalseWhenClassDoesNotImplementInterfaceType() =>
            Assert.IsTrue(typeof(ParentClass).IsImplementingInterface(typeof(ITestInterface)));
    }

    /// <summary>
    ///     This class is used soley for testing purposes.
    /// </summary>
    [CustomTest]
    class BaseClass : ITestInterface
    {
        [CustomTest]
        public virtual int PropertyMarkedWithAttribute { get; set; }

        public virtual int PropertyNotMarkedWithAttribute { get; set; }
    }

    /// <summary>
    ///     This class is used soley for testing purposes.
    /// </summary>
    class ParentClass : BaseClass
    {
        public override int PropertyMarkedWithAttribute { get; set; }

        public override int PropertyNotMarkedWithAttribute { get; set; }
    }

    /// <summary>
    ///     This class is used soley for testing purposes.
    /// </summary>
    class CustomTestAttribute : Attribute { }

    /// <summary>
    ///     This interface is used soley for testing purposes.
    /// </summary>
    interface ITestInterface { }
}
