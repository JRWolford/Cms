using Cms.NavigationService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NavigationService.Contracts
{
    public interface INavigationService
    {
        void Navigate(Destination destination, string target);

        void Navigate(string regionName, string target);

        void ClearRegion(Destination destination);

        void ClearRegion(string regionName);
    }
}
