using Cms.Core.Wpf.Enums;
using Prism.Events;

namespace Cms.Core.Wpf.Events
{
    /// <summary>
    ///     Custom event for menu navigation. This event will either show or hide the menu flyout
    ///     based on the <see cref="Navigate"/> enum value provided.
    /// </summary>
    public class MenuNavigationEvent : PubSubEvent<Navigate>
    {
    }
}
