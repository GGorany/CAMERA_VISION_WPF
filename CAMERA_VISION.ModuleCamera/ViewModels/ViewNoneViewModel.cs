using Prism.Mvvm;

namespace CAMERA_VISION.ModuleCamera.ViewModels
{
    public class ViewNoneViewModel : BindableBase
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewNoneViewModel()
        {
            Message = "카메라를 찾을 수 없습니다.";
        }
    }
}
