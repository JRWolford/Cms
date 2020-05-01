using Prism.Events;

namespace Cms.Core.Wpf.Events
{
    /// <summary>
    ///     Event that is raised whenever the user engages or disengages with the navigation blade.
    /// </summary>
    public class NavigationBladeEngagedEvent : PubSubEvent<bool>
    {
    }
}
