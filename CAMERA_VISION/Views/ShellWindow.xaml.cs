using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using CAMERA_VISION.ViewModels;
using System.Windows;

namespace CAMERA_VISION.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        private void On_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ShellWindowViewModel).DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
