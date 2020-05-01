using Cms.Core.Wpf.Utils;
using Cms.Desktop.Views;
using Cms.DialogService.Contracts;
using Cms.DisplaySettingsService;
using Cms.DisplaySettingsService.Contracts;
using Cms.DisplaySettingsService.Data;
using Cms.Modules.Navigation;
using Cms.Modules.Navigation.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Linq;
using System.Windows;

namespace Cms.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.PrimaryNavigationRegion, typeof(PrimaryNavigation));
            regionManager.RegisterViewWithRegion(RegionNames.SettingsNavigationPanel, typeof(SettingsNavigation));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule(typeof(NavigationModule));
            //moduleCatalog.AddModule(typeof(SettingsModule));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(IDisplaySettingsUnitOfWork), typeof(DisplaySettingsUnitOfWork));

            containerRegistry.Register(typeof(IDisplaySettingsManager), typeof(DisplaySettingsManager));
            containerRegistry.Register(typeof(IDisplaySettingsInitializer), typeof(DisplaySettingsManager));

            containerRegistry.Register(typeof(IDialogService), typeof(DialogService.DialogService));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var dispSettingsInitializer = Container.Resolve<IDisplaySettingsInitializer>();
            dispSettingsInitializer.Initialize();

            //var dispSettingsManager = Container.Resolve<IDisplaySettingsManager>();
            //dispSettingsManager.SelectAccent(dispSettingsManager
            //    .GetAvailableAccentColorOptions().FirstOrDefault(a => a.Id == 2));

            //dispSettingsManager.SelectTheme(dispSettingsManager
            //    .GetAvailableThemeOptions().FirstOrDefault(t => t.Id == 2));
        }
    }
}
