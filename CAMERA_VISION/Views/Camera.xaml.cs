using Prism.Modularity;
using System.Windows;
using System.Windows.Controls;

namespace CAMERA_VISION.Views
{
    /// <summary>
    /// Camera.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Camera : UserControl
    {
        readonly IModuleManager _moduleManager;

        public Camera(IModuleManager moduleManager)
        {
            InitializeComponent();
            _moduleManager = moduleManager;
        }

        private void On_Loaded(object sender, RoutedEventArgs e)
        {
            _moduleManager.LoadModule("ModuleCam");
        }
    }
}
