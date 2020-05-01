using Cms.Core.Wpf.Contracts;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace Cms.Modules.Navigation.Utils
{
    /// <summary>
    ///     The navigation item object model.
    /// </summary>
    internal sealed class NavigationItem : BindableBase, INavigationItem
    {
        #region Private Fields

        private string _destinationRegionName;

        private Type _destinationView;

        private bool _isDestinationActive;

        private bool _isNavigationAvailable;

        private string _name;

        private Dictionary<string, object> _navigationParameters;

        private string _toolTip;

        #endregion

        #region Properties

        /// <inheritdoc cref="INavigationItem.DestinationRegionName"/>
        public string DestinationRegionName
        {
            get => _destinationRegionName;
            set => SetProperty(ref _destinationRegionName, value);
        }

        /// <inheritdoc cref="INavigationItem.DestinationView"/>
        public Type DestinationView
        {
            get => _destinationView;
            set => SetProperty(ref _destinationView, value);
        }

        /// <inheritdoc cref="INavigationItem.IsDestinationActive"/>
        public bool IsDestinationActive
        {
            get => _isDestinationActive;
            set => SetProperty(ref _isDestinationActive, value);
        }

        /// <inheritdoc cref="INavigationItem.IsNavigationAvailable"/>
        public bool IsNavigationAvailable
        {
            get => _isNavigationAvailable;
            set => SetProperty(ref _isNavigationAvailable, value);
        }

        /// <inheritdoc cref="INavigationItem.Name"/>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <inheritdoc cref="INavigationItem.NavigationParameters"/>
        public Dictionary<string, object> NavigationParameters
        {
            get => _navigationParameters;
            set => SetProperty(ref _navigationParameters, value);
        }

        /// <inheritdoc cref="INavigationItem.ToolTip"/>
        public string ToolTip
        {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);
        }

        #endregion
    }
}
