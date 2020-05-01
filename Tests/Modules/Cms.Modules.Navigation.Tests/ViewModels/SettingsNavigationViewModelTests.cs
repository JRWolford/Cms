using Cms.Core.Wpf.Contracts;
using Cms.Modules.Navigation.ViewModels;
using Cms.Tests.Core.TestBases;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Modules.Navigation.Tests.ViewModels
{
    /// <summary>
    ///     This test fixture contains all tests for the <see cref="SettingsNavigationViewModel"/> class.
    /// </summary>
    public sealed class SettingsNavigationViewModelTests : ViewModelTestBase<SettingsNavigationViewModel>
    {
        /// <summary>
        ///     Asserts that the <see cref="SettingsNavigationViewModel.NavigationItems"/> property is not
        ///     null when the class is constructed.
        /// </summary>
        [Test]
        public void NavigationItemsShouldNotBeNullOnConstruction()
        {
            //Arrange
            var regionManagerMock = new Mock<IRegionManager>();

            //Act
            var viewModel = new SettingsNavigationViewModel(regionManagerMock.Object);

            //Assert
            Assert.IsNotNull(viewModel.NavigationItems);
        }

        /// <summary>
        ///     Asserts that the <see cref="SettingsNavigationViewModel.NavigateCommand.CanExecute"/>
        ///     returns false when the <see cref="INavigationItem.IsNavigationAvailable"/> parameter
        ///     property is false.
        /// </summary>
        [Test]
        public void NavigateCommandShouldNotExecuteWhenNavigationParameterIsUnavailable()
        {
            //Arrange
            var regionManagerMock = new Mock<IRegionManager>();
            var navItemMock = new Mock<INavigationItem>();
            navItemMock.SetupGet(n => n.IsNavigationAvailable).Returns(false);

            var viewModel = new SettingsNavigationViewModel(regionManagerMock.Object);

            //Assert
            Assert.IsFalse(viewModel.NavigateCommand.CanExecute(navItemMock.Object));
        }

        /// <summary>
        ///     Asserts that the <see cref="SettingsNavigationViewModel.NavigateCommand.CanExecute"/>
        ///     returns true when the <see cref="INavigationItem.IsNavigationAvailable"/> parameter
        ///     property is true.
        /// </summary>
        [Test]
        public void NavigateCommandShouldExecuteWhenNavigationParameterIsAvailable()
        {
            //Arrange
            var regionManagerMock = new Mock<IRegionManager>();
            var navItemMock = new Mock<INavigationItem>();
            navItemMock.SetupGet(n => n.IsNavigationAvailable).Returns(true);

            var viewModel = new SettingsNavigationViewModel(regionManagerMock.Object);

            //Assert
            Assert.IsTrue(viewModel.NavigateCommand.CanExecute(navItemMock.Object));
        }

        /// <summary>
        ///     Asserts that all <see cref="SettingsNavigationViewModel.NavigationItems"/> are set 
        ///     as unavailable except for the navigation item being navigated to.
        /// </summary>
        [Test]
        public void NavigateCommandShouldMakeAllNavigationItemsExceptForTheItemBeingNavigatedToInactive()
        {
            //Arrange
            var regionManagerMock = new Mock<IRegionManager>();

            var viewModel = new SettingsNavigationViewModel(regionManagerMock.Object);
            foreach (var navItem in viewModel.NavigationItems)
            {
                navItem.IsNavigationAvailable = true;
                navItem.IsDestinationActive = true;
            }

            var destinationNavItem = viewModel.NavigationItems[0];

            //Act
            viewModel.NavigateCommand.Execute(destinationNavItem);

            //Assert
            foreach (var navItem in viewModel.NavigationItems.Where(n => n != destinationNavItem))
                Assert.IsFalse(navItem.IsDestinationActive);
        }

        /// <summary>
        ///     Asserts that all <see cref="SettingsNavigationViewModel.NavigationItems"/> are set 
        ///     as unavailable except for the navigation item being navigated to.
        /// </summary>
        [Test]
        public void NavigateCommandShouldMakeTheItemBeingNavigatedToActive()
        {
            //Arrange
            var regionManagerMock = new Mock<IRegionManager>();

            var viewModel = new SettingsNavigationViewModel(regionManagerMock.Object);
            foreach (var navItem in viewModel.NavigationItems)
            {
                navItem.IsNavigationAvailable = true;
                navItem.IsDestinationActive = false;
            }

            //Act
            viewModel.NavigateCommand.Execute(viewModel.NavigationItems[0]);

            //Assert
            Assert.IsTrue(viewModel.NavigationItems[0].IsDestinationActive);
        }

        [Test]
        public void NavigateCommandShouldRequestNavigationWhenExecuted()
        {
            //Arrange
            bool isNavigationRequested = false;

            var navItemMock = new Mock<INavigationItem>();
            navItemMock.SetupGet(n => n.IsNavigationAvailable)
                .Returns(true);
            navItemMock.SetupGet(n => n.DestinationView)
                .Returns(GetType());

            var regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.Setup(r => r.RequestNavigate(It.IsAny<string>(), It.IsAny<Uri>()))
                .Callback(() =>isNavigationRequested = true);

            var viewModel = new SettingsNavigationViewModel(regionManagerMock.Object);

            //Act
            viewModel.NavigateCommand.Execute(navItemMock.Object);

            //Assert
            Assert.IsTrue(isNavigationRequested);
        }

    }
}
