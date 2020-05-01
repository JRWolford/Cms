using System.Collections.Generic;

namespace Cms.NavigationService.Utils
{
    /// <summary>
    ///     This class contains all prism region names for navigation.
    /// </summary>
    public static class RegionDictionary
    {
        public static Dictionary<Destination, string> RegionsDictionary => new Dictionary<Destination, string>
        {
            {Destination.PrimaryContentRegion, PrimaryContentRegion }
        };

        public const string PrimaryContentRegion = "";
    }
}
