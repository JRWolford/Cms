using Cms.Core.Wpf.Enums;
using Cms.Core.Wpf.Events;
using Cms.Modules.Navigation.ViewModels;
using Cms.Tests.Core.TestBases;
using Moq;
using NUnit.Framework;
using Prism.Events;
using System;

namespace Cms.Modules.Navigation.Tests.ViewModels
{
    /// <summary>
    ///     This test fixture contains all tests for the <see cref="PrimaryNavigationViewModel"/> class.
    /// </summary>
    [TestFixture]
    public sealed class PrimaryNavigationViewModelTests : ViewModelTestBase<PrimaryNavigationViewModel>
    {
        /// <summary>
        ///     Asserts that the <see cref="PrimaryNavigationViewModel"/> class subscribes to the 
        ///     <see cref="NavigationBladeEngagedEvent"/> when it is constructed.
        /// </summary>
        [Test]
        public void ShouldSubscribetToNavigationBladEngagedEventOnConstruction()
        {
            //Arrange
            bool callback = false;

            var navBladEngagedEventMock = new Mock<NavigationBladeEngagedEvent>();
            navBladEngagedEventMock.Setup(n => n.Subscribe(It.IsAny<Action<bool>>(), It.IsAny<ThreadOption>(), It.IsAny<bool>(), It.IsAny<Predicate<bool>>()))
                .Callback(() => callback = true);

            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(e => e.GetEvent<NavigationBladeEngagedEvent>())
                .Returns(navBladEngagedEventMock.Object);

            //Act
            var viewModel = new PrimaryNavigationViewModel(eventAggregatorMock.Object);

            //Assert
            Assert.IsTrue(callback);
        }

        /// <summary>
        ///     Asserts that the <see cref="PrimaryNavigationViewModel.OpenMenuCommand"/> raises the
        ///     <see cref="MenuNavigationEvent"/> when executed.
        /// </summary>
        [Test]
        public void OpenMenuCommandShouldRaiseMenuNavigationEventWithNavigateToPayload()
        {
            //Arrange
            bool isEventPublished = false;

            var navBladEngagedEventMock = new Mock<NavigationBladeEngagedEvent>();
            navBladEngagedEventMock.Setup(n => n.Subscribe(It.IsAny<Action<bool>>(), It.IsAny<ThreadOption>(), It.IsAny<bool>(), It.IsAny<Predicate<bool>>()));
            navBladEngagedEventMock.Setup(n => n.Unsubscribe(It.IsAny<SubscriptionToken>()));

            var menuNavEventMock = new Mock<MenuNavigationEvent>();
            menuNavEventMock.Setup(m => m.Publish(Navigate.To))
                .Callback(() => isEventPublished = true);

            var eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(e => e.GetEvent<NavigationBladeEngagedEvent>())
                .Returns(navBladEngagedEventMock.Object);
            eventAggregator.Setup(e => e.GetEvent<MenuNavigationEvent>())
                .Returns(menuNavEventMock.Object);

            var viewModel = new PrimaryNavigationViewModel(eventAggregator.Object);

            //Act
            viewModel.OpenMenuCommand.Execute();

            //Assert
            Assert.IsTrue(isEventPublished);
        }

        /// <summary>
        ///     Asserts that the view model unsubscribes from the <see cref="NavigationBladeEngagedEvent"/>
        ///     when it is disposed.
        /// </summary>
        [Test]
        public void DisposingOfViewModelShouldUnsubscribeFromNavigationBladeEngagedEvent()
        {
            //Arrange
            var navBladEngagedEventMock = new Mock<NavigationBladeEngagedEvent>();
            navBladEngagedEventMock.Setup(n => n.Subscribe(It.IsAny<Action<bool>>(), It.IsAny<ThreadOption>(), It.IsAny<bool>(), It.IsAny<Predicate<bool>>()));             
            navBladEngagedEventMock.Setup(n => n.Unsubscribe(It.IsAny<SubscriptionToken>()));

            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(e => e.GetEvent<NavigationBladeEngagedEvent>())
                .Returns(navBladEngagedEventMock.Object);

            //Act
            var viewModel = new PrimaryNavigationViewModel(eventAggregatorMock.Object);
            viewModel.Dispose();

            //Assert
            navBladEngagedEventMock.Verify(n => n.Unsubscribe(It.IsAny<SubscriptionToken>()), Times.Once);
        }
    }
}
