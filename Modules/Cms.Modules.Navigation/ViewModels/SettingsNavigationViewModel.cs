using Cms.Core.Wpf.Contracts;
using Cms.Core.Wpf.Utils;
using Cms.Modules.Navigation.Utils;
using Prism.Commands;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cms.Modules.Navigation.ViewModels
{
    public sealed class SettingsNavigationViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IRegionManager _regionManager;

        private ObservableCollection<INavigationItem> _navigationItems;

        #endregion

        #region Constructors

        public SettingsNavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigationItems = new ObservableCollection<INavigationItem>(GetSettingsNavigationItems());
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The available navigation items that the settings navigation system can navigate to.
        /// </summary>
        public ObservableCollection<INavigationItem> NavigationItems
        {
            get => _navigationItems;
            private set => SetProperty(ref _navigationItems, value);
        }

        #endregion

        #region Commands

        /// <summary>
        ///     The command for navigating to the <see cref="INavigationItem"/> command parameter.
        /// </summary>
        public DelegateCommand<INavigationItem> NavigateCommand =>
            new DelegateCommand<INavigationItem>(Navigate, (i) => i.IsNavigationAvailable);

        #endregion

        #region Methods

        /// <summary>
        ///     Returns the available settings navigation items.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="INavigationItem"/> objects.
        /// </returns>
        private List<INavigationItem> GetSettingsNavigationItems()
        {
            var navItems = new List<INavigationItem>
            {
                new NavigationItem { Name="System", ToolTip="Information about the applciation system.", IsDestinationActive = false, IsNavigationAvailable = false},
                new NavigationItem { Name="Settings", ToolTip="All application settings.", IsDestinationActive = false, IsNavigationAvailable = true},
            };

            return navItems;
        }

        /// <summary>
        ///     Navigates to the specified navigation item.
        /// </summary>
        /// <param name="navItem">
        ///     The item that should be navigated to.
        /// </param>
        private void Navigate(INavigationItem navItem)
        {
            foreach (var item in NavigationItems)
                item.IsDestinationActive = false;
            
            _regionManager.RequestNavigate(navItem.DestinationRegionName, navItem.DestinationView.FullName);
            
            navItem.IsDestinationActive = true;
        }

        #endregion
    }
}
