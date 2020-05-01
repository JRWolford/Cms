using Cms.Core.Wpf.Utils;
using NUnit.Framework;

namespace Cms.Tests.Core.TestBases
{
    /// <summary>
    ///     This is the testing base for a view model class.
    /// </summary>
    /// <typeparam name="TViewModel">
    ///     The type of view model being tested.
    /// </typeparam>
    public abstract class ViewModelTestBase<TViewModel> where TViewModel : class
    {
        /// <summary>
        ///     Asserts that the view model class is sealed.
        /// </summary>
        [Test]
        public void ViewModelShouldBeSealed() =>
            Assert.That(typeof(TViewModel).IsSealed);

        /// <summary>
        ///     Asserts that the view model inherits from the <see cref="ViewModelBase"/> class.
        /// </summary>
        [Test]
        public void ViewModelShouldInheritFromViewModelBase() =>
            Assert.That(typeof(TViewModel).IsSubclassOf(typeof(ViewModelBase)));
    }
}
