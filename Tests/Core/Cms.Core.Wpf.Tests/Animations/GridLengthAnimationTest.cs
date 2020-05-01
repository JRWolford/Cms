using Cms.Core.Wpf.Animations;
using NUnit.Framework;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Cms.Core.Wpf.Tests.Animations
{
    /// <summary>
    ///     This test fixture contains all tests for the <see cref="GridLengthAnimation"/> class.
    /// </summary>
    [TestFixture]
    public sealed class GridLengthAnimationTest
    {
        /// <summary>
        ///     Asserts that the <see cref="GridLengthAnimation"/> class is sealed.
        /// </summary>
        [Test]
        public void ClassShouldBeSealed() =>
            Assert.That(typeof(GridLengthAnimation).IsSealed);

        /// <summary>
        ///     Asserts that the <see cref="GridLengthAnimation"/> class inherits from the
        ///     <see cref="AnimationTimeline"/> class.
        /// </summary>
        [Test]
        public void ClassShouldInheritFromAnimationTimeline() =>
            Assert.That(typeof(GridLengthAnimation).IsSubclassOf(typeof(AnimationTimeline)));

        /// <summary>
        ///     Asserts that the <see cref="GridLengthAnimation.TargetPropertyType"/> is targeting
        ///     a <see cref="GridLength"/> type.
        /// </summary>
        [Test]
        public void TargetPropertyTypeShouldReturnTypeOfGridLength()
        {
            //Arrange
            var animation = new GridLengthAnimation();

            //Act
            Type targetPropType = animation.TargetPropertyType;

            //Assert
            Assert.AreEqual(typeof(GridLength), targetPropType);
        }

        //ToDo: Finish testing.
    }
}
