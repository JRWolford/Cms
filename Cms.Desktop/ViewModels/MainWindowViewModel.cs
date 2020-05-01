using Cms.Core.Wpf.Enums;
using Cms.Core.Wpf.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using Unity;

namespace Cms.Desktop.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Private Fields

        private readonly IUnityContainer _container;

        private readonly IEventAggregator _eventAggregator;

        private bool _isSettingsMenuFlyoutOpen;

        private bool _isNotificationFlyoutOpen;

        private SubscriptionToken _menuNavigationEventToken;

        #endregion

        #region Constructors

        public MainWindowViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;

            _menuNavigationEventToken = _eventAggregator.GetEvent<MenuNavigationEvent>().Subscribe(MenuNavigationEventHandler);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Indicates if the settings menu flyout is open or not.
        /// </summary>
        public bool IsSettingsMenuFlyoutOpen
        {
            get => _isSettingsMenuFlyoutOpen;
            set => SetProperty(ref _isSettingsMenuFlyoutOpen, value);
        }

        /// <summary>
        ///     Indicates if the notifications flyout is open or not.
        /// </summary>
        public bool IsNotificationFlyoutOpen
        {
            get => _isNotificationFlyoutOpen;
            set => SetProperty(ref _isNotificationFlyoutOpen, value);
        }

        /// <summary>
        ///     The title of the application. This is displayed on the application's window header.
        /// </summary>
        public string Title => "Pocket Watch";

        #endregion

        #region Commands

        /// <summary>
        ///     The command for opening a notification
        /// </summary>
        //public DelegateCommand<List<INotificationContent>> OpenNotificationsCommand =>
        //    new DelegateCommand<List<INotificationContent>>(OpenNotifications);

        #endregion

        #region Methods

        /// <summary>
        ///     The handler for the <see cref="MenuNavigationEvent"/>.
        /// </summary>
        /// <param name="navigate">
        ///     Indicates if navigation is towards or away from the settings menu.
        /// </param>
        private void MenuNavigationEventHandler(Navigate navigate)
        {
            switch (navigate)
            {
                case Navigate.To:
                    IsSettingsMenuFlyoutOpen = true;
                    break;
                case Navigate.From:
                    IsSettingsMenuFlyoutOpen = false;
                    break;
                default:
                    IsSettingsMenuFlyoutOpen = false;
                    break;
            }
        }

        //private void OpenNotifications(List<INotificationContent> notifications)
        //{
        //    var navParams = new NavigationParameters { { "notifications", notifications } };
        //    var regionManager = _container.Resolve<IRegionManager>();
        //    regionManager.RequestNavigate(RegionNames.NotificationHubFlyoutRegion, typeof(NotificationsHub).FullName, navParams);
        //    IsNotificationFlyoutOpen = true;
        //}


        private void OpenSettingsMenu()
        {
            IsSettingsMenuFlyoutOpen = true;
        }

        /// <summary>
        ///     Helper method for publishing a <see cref="NavigationBladeEngagedEvent"/> with a payload of
        ///     <paramref name="isBladeEngaged"/>.
        /// </summary>
        /// <param name="isBladeEngaged"></param>
        public void PublishNavigationBladeEngagementStatus(bool isBladeEngaged) =>
            _eventAggregator.GetEvent<NavigationBladeEngagedEvent>().Publish(isBladeEngaged);

        #endregion
    }
}
