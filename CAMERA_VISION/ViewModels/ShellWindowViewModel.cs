using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace CAMERA_VISION.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "CAMERA VISION";
        public string Title {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IDialogCoordinator _dialogCoordinator;
        public IDialogCoordinator DialogCoordinator {
            get { return _dialogCoordinator; }
            set { _dialogCoordinator = value; }
        }

        #region Commands
        private ICommand _CommandLoaded;
        private DelegateCommand _CommandHome;
        private DelegateCommand _CommandMainPanel;
        private DelegateCommand _CommandCamera;

        public ICommand CommandLoaded => _CommandLoaded ?? (_CommandLoaded = new DelegateCommand(OnLoaded));
        public DelegateCommand CommandHome => _CommandHome ?? (_CommandHome = new DelegateCommand(HomeRegion));
        public DelegateCommand CommandMainPanel => _CommandMainPanel ?? (_CommandMainPanel = new DelegateCommand(MainPanelRegion));
        public DelegateCommand CommandCamera => _CommandCamera ?? (_CommandCamera = new DelegateCommand(CameraRegion));
        #endregion

        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        void OnLoaded()
        {
            HomeRegion();
        }

        void HomeRegion()
        {
            _regionManager.RequestNavigate("MainPageRegion", "Home");
        }

        void MainPanelRegion()
        {
            _regionManager.RequestNavigate("MainPageRegion", "MainPanel");
        }

        void CameraRegion()
        {
            _regionManager.RequestNavigate("MainPageRegion", "Camera");
        }
    }
}
