using System;
using System.Collections.Generic;

namespace Cms.Core.Wpf.Contracts
{
    /// <summary>
    ///     The base contract for a navigation item.
    /// </summary>
    public interface INavigationItem
    {
        /// <summary>
        ///     The Prism <see cref="IRegion" /> index name of the region where the
        ///     <see cref="DestinationView" /> is to be injected.
        /// </summary>
        string DestinationRegionName { get; set; }

        /// <summary>
        ///     The actual destination of the navigation item.
        /// </summary>
        /// <remarks>
        ///     This should be a Prism View.
        /// </remarks>
        Type DestinationView { get; set; }

        /// <summary>
        ///     Indicates if the <see cref="DestinationView" /> is already active in the region
        ///     specified by the <see cref="DestinationRegionName" />.
        /// </summary>
        bool IsDestinationActive { get; set; }

        /// <summary>
        ///     Indicates if the <see cref="INavigationItem" /> can be navigated to or not.
        /// </summary>
        bool IsNavigationAvailable { get; set; }

        /// <summary>
        ///     The short name of the navigation item which indicates to the user the
        ///     destination.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     A collection of navigation parameters to be passed via Prism.
        /// </summary>
        Dictionary<string, object> NavigationParameters { get; set; }

        /// <summary>
        ///     A descriptive tool tip which provides the user with more information
        ///     about the destination.
        /// </summary>
        string ToolTip { get; set; }
    }
}
