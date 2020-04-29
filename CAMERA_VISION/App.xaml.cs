using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;

using CAMERA_VISION.Views;
using CAMERA_VISION.ModuleCamera;

using System.Windows;

namespace CAMERA_VISION
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Home>();
            containerRegistry.RegisterForNavigation<MainPanel>();
            containerRegistry.RegisterForNavigation<Camera>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            var moduleAType = typeof(ModuleCam);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = moduleAType.Name,
                ModuleType = moduleAType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });
        }
    }
}
