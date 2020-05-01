using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Cms.Core.Wpf.Animations
{
    /// <summary>
    ///     A custom animation for adjusting the grid length of an object.
    /// </summary>
    public sealed class GridLengthAnimation : AnimationTimeline
    {
        #region Dependency Properties

        /// <summary>
        ///     Dependency property for the <see cref="From"/> property of the animation for xaml bindings.
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            nameof(From), typeof(GridLength), typeof(GridLengthAnimation));

        /// <summary>
        ///     Dependency property for the <see cref="To"/> property of the animation for xaml bindings.
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            nameof(To), typeof(GridLength), typeof(GridLengthAnimation));

        #endregion

        #region Properties

        /// <summary>
        ///     Indicates the starting position of the <see cref="GridLength"/> value being animated.
        /// </summary>
        public GridLength From
        {
            get => (GridLength)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        /// <summary>
        ///     The type of property being animated.
        /// </summary>
        public override Type TargetPropertyType => typeof(GridLength);

        /// <summary>
        ///     Indicates the ending position of the <see cref="GridLength"/> value being animated.
        /// </summary>
        public GridLength To
        {
            get => (GridLength)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates a new instance of the <see cref="GridLengthAnimation"/> for freezing.
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore() => 
            new GridLengthAnimation();

        /// <summary>
        ///     Gets the current value of the <see cref="GridLength"/> being animated.
        /// </summary>
        /// <param name="defaultOriginValue">
        ///     The default original value of the <see cref="GridLength"/> being animated.
        /// </param>
        /// <param name="defaultDestinationValue">
        ///     The default final value of the <see cref="GridLength"/> being animated.
        /// </param>
        /// <param name="animationClock">
        ///     The current clock value of the animation.
        /// </param>
        /// <returns>
        ///     The correct current value of the <see cref="GridLength"/> being animated based on the <see cref="From"/> and
        ///     <see cref="To"/> properties and the <see cref="AnimationClock"/>.
        /// </returns>
        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromValue = this.From.Value;
            double toValue = this.To.Value;

            if (fromValue > toValue)
                return new GridLength((1 - animationClock.CurrentProgress.Value) * (fromValue - toValue) + toValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
                
            return new GridLength((animationClock.CurrentProgress.Value) * (toValue - fromValue) + fromValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
        }

        #endregion
    }
}
