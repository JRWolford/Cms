using Cms.NavigationService.Contracts;
using Cms.NavigationService.Utils;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NavigationService
{
    public class NavigationService : INavigationService
    {
        private readonly IRegionManager _regionManager;

        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ClearRegion(Destination destination)
        {
            throw new NotImplementedException();
        }

        public virtual void ClearRegion(string regionName)
        {
            throw new NotImplementedException();
        }

        public void Navigate(Destination destination, string target)
        {
            throw new NotImplementedException();
        }

        public void Navigate(string regionName, string target)
        {
            throw new NotImplementedException();
        }
    }
}
