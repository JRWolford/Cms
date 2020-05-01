using Cms.Modules.Settings.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Cms.Modules.Settings
{
    public class SettingsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<object, DisplaySettings>(typeof(DisplaySettings).FullName);
        }
    }
}