using Cms.Core.Wpf.Enums;
using Cms.Core.Wpf.Events;
using Cms.Core.Wpf.Utils;
using Prism.Commands;
using Prism.Events;

namespace Cms.Modules.Navigation.ViewModels
{
    public sealed class PrimaryNavigationViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IEventAggregator _eventAggregator;

        private readonly SubscriptionToken _navigationBladeEnagedToken;

        private bool _isNavigationBladeOpen;

        #endregion

        #region Constructors

        public PrimaryNavigationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _navigationBladeEnagedToken = _eventAggregator
                .GetEvent<NavigationBladeEngagedEvent>().Subscribe(OnBladeEngaged);

            Disposals = () =>
            {
                _eventAggregator.GetEvent<NavigationBladeEngagedEvent>().Unsubscribe(_navigationBladeEnagedToken);
            };
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Indicates if the navigation blade is open or not.
        /// </summary>
        public bool IsNavigationBladeOpen
        {
            get => _isNavigationBladeOpen;
            set => SetProperty(ref _isNavigationBladeOpen, value);
        }

        #endregion

        #region Commands

        /// <summary>
        ///     The command for opening the main menu systems menu.
        /// </summary>
        public DelegateCommand OpenMenuCommand =>
            new DelegateCommand(OpenMenu);

        #endregion

        #region Methods

        /// <summary>
        ///     The event handler for the <see cref="NavigationBladeEngagedEvent"/>. It sets the <see cref="IsNavigationBladeOpen"/>
        ///     property to the payload value, <paramref name="isBladeOpen"/>.
        /// </summary>
        /// <param name="isBladeOpen">
        ///     The payload of the <see cref="NavigationBladeEngagedEvent"/>, which indicates if the blade is open (true) or not (false).
        /// </param>
        private void OnBladeEngaged(bool isBladeOpen) =>
            IsNavigationBladeOpen = isBladeOpen;

        /// <summary>
        ///     The execute method of the <see cref="OpenMenuCommand"/>, it opens up the settings menu flyout 
        ///     (by publishing the <see cref="MenuNavigationEvent"/>), and navigates to the settings navigation.
        /// </summary>
        private void OpenMenu() =>
            _eventAggregator.GetEvent<MenuNavigationEvent>().Publish(Navigate.To);
        
        #endregion
    }
}
