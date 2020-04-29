using Basler.Pylon;

using Prism.Commands;
using Prism.Mvvm;

using CAMERA_VISION.ModuleCamera.Helpers;
using CAMERA_VISION.ModuleCamera.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CAMERA_VISION.ModuleCamera.ViewModels
{
    public class ViewBaslerViewModel : BindableBase
    {
        private string _Name;
        public string Name {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        public ICameraInfo Tag { get; set; }

        private string _TagText;
        public string TagText {
            get { return _TagText; }
            set { SetProperty(ref _TagText, value); }
        }

        private double _FrameRate;
        public double FrameRate
        {
            get { return _FrameRate; }
            set { SetProperty(ref _FrameRate, value); }
        }

        // Camera
        private Camera camera = null;
        private PixelDataConverter converter = null;
        Dispatcher dispatcher = Application.Current.Dispatcher;

        #region Properties Camera Image
        private BitmapImage _CameraImage;
        public BitmapImage CameraImage
        {
            get { return _CameraImage; }
            set { SetProperty(ref _CameraImage, value); }
        }
        #endregion

        #region Properties Binding Models
        private BaslerParam _Param;
        public BaslerParam Param
        {
            get { return _Param; }
            set { SetProperty(ref _Param, value); }
        }

        private bool _EnParamAll;
        public bool EnParamAll
        {
            get { return _EnParamAll; }
            set { SetProperty(ref _EnParamAll, value); }
        }

        private BaslerLog _Log;
        public BaslerLog Log
        {
            get { return _Log; }
            set { SetProperty(ref _Log, value); }
        }
        #endregion

        #region Peoperties Button Enable
        private bool _EnShotOnce;
        public bool EnShotOnce {
            get { return _EnShotOnce; }
            set { SetProperty(ref _EnShotOnce, value); }
        }

        private bool _EnShotContinuous;
        public bool EnShotContinuous {
            get { return _EnShotContinuous; }
            set { SetProperty(ref _EnShotContinuous, value); }
        }

        private bool _EnShotStop;
        public bool EnShotStop {
            get { return _EnShotStop; }
            set { SetProperty(ref _EnShotStop, value); }
        }

        private bool _EnResetCamera;
        public bool EnResetCamera {
            get { return _EnResetCamera; }
            set { SetProperty(ref _EnResetCamera, value); }
        }

        private bool _EnConnect;
        public bool EnConnect {
            get { return _EnConnect; }
            set { SetProperty(ref _EnConnect, value); }
        }

        private bool _EnDisConnet;
        public bool EnDisConnet {
            get { return _EnDisConnet; }
            set { SetProperty(ref _EnDisConnet, value); }
        }

        private bool _EnParameterSave;
        public bool EnParameterSave {
            get { return _EnParameterSave; }
            set { SetProperty(ref _EnParameterSave, value); }
        }

        private bool _EnParameterLoad;
        public bool EnParameterLoad {
            get { return _EnParameterLoad; }
            set { SetProperty(ref _EnParameterLoad, value); }
        }

        private bool _EnImageSave;
        public bool EnImageSave {
            get { return _EnImageSave; }
            set { SetProperty(ref _EnImageSave, value); }
        }

        private bool _EnImageSaveAuto;
        public bool EnImageSaveAuto {
            get { return _EnImageSaveAuto; }
            set { SetProperty(ref _EnImageSaveAuto, value); }
        }

        private bool _IsImageSaveAuto;
        public bool IsImageSaveAuto {
            get { return _IsImageSaveAuto; }
            set { SetProperty(ref _IsImageSaveAuto, value); }
        }
        #endregion

        #region DelegateCommand
        private DelegateCommand _ShotOnce;
        public DelegateCommand ShotOnce => _ShotOnce ?? (_ShotOnce = new DelegateCommand(Shot_Once));

        private DelegateCommand _ShotContinuous;
        public DelegateCommand ShotContinuous => _ShotContinuous ?? (_ShotContinuous = new DelegateCommand(Shot_Continuous));

        private DelegateCommand _ShotStop;
        public DelegateCommand ShotStop => _ShotStop ?? (_ShotStop = new DelegateCommand(Shot_Stop));

        private DelegateCommand _ResetCamera;
        public DelegateCommand ResetCamera => _ResetCamera ?? (_ResetCamera = new DelegateCommand(Reset_Camera));

        private DelegateCommand _Connect;
        public DelegateCommand Connect => _Connect ?? (_Connect = new DelegateCommand(Connect_Camera));

        private DelegateCommand _DisConnet;
        public DelegateCommand DisConnet => _DisConnet ?? (_DisConnet = new DelegateCommand(DisConnect_Camera));

        private DelegateCommand _ParameterSave;
        public DelegateCommand ParameterSave => _ParameterSave ?? (_ParameterSave = new DelegateCommand(Parameter_Save));

        private DelegateCommand _ParameterLoad;
        public DelegateCommand ParameterLoad => _ParameterLoad ?? (_ParameterLoad = new DelegateCommand(Parameter_Load));

        private DelegateCommand _ImageSave;
        public DelegateCommand ImageSave => _ImageSave ?? (_ImageSave = new DelegateCommand(Image_Save));
        #endregion

        #region Constructor
        public ViewBaslerViewModel()
        {
            Log = new BaslerLog();

            if (camera != null)
            {
                DestroyCamera();
            }

            EnParamAll = false;

            EnShotOnce = false;
            EnShotContinuous = false;
            EnShotStop = false;
            EnResetCamera = false;
            EnConnect = true;
            EnDisConnet = false;
            EnParameterSave = false;
            EnParameterLoad = false;

            IsImageSaveAuto = false;
            EnImageSave = false;
            EnImageSaveAuto = false;

            dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
        }
        #endregion

        #region Private Methods - Buttons
        private void Shot_Once()
        {
            try
            {
                // Starts the grabbing of one image.
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception e)
            {
                Log.Set(e.Message);
            }
        }

        private void Shot_Continuous()
        {
            try
            {
                // Start the grabbing of images until grabbing is stopped.
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception e)
            {
                Log.Set(e.Message);
            }
        }

        private void Shot_Stop()
        {
            // Stop the grabbing.
            try
            {
                camera.StreamGrabber.Stop();
            }
            catch (Exception e)
            {
                Log.Set(e.Message);
            }
        }

        private void Reset_Camera()
        {
            try
            {
                Param.ResetDevice();
                Log.Set("Camera is reset now.");
                DisConnect_Camera();
            }
            catch (Exception e)
            {
                Log.Set(e.Message);
            }
        }

        private void Connect_Camera()
        {
            if (camera != null)
            {
                DestroyCamera();
            }

            try
            {
                // Create a new camera object.
                camera = new Camera(Tag);

                camera.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                camera.ConnectionLost += OnConnectionLost;
                camera.CameraOpened += OnCameraOpened;
                camera.CameraClosed += OnCameraClosed;
                camera.StreamGrabber.GrabStarted += OnGrabStarted;
                camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                camera.StreamGrabber.GrabStopped += OnGrabStopped;

                // Open the connection to the camera device.
                camera.Open();

                if (camera.IsOpen)
                {
                    Param = new BaslerParam(camera);
                    EnParamAll = true;

                    converter = new PixelDataConverter();
                }
            }
            catch (Exception e)
            {
                Log.Set(e.Message);
            }
        }

        private void DisConnect_Camera()
        {
            DestroyCamera();

            Log.Set("카메라 연결을 해제했습니다.");
        }

        private void Parameter_Save()
        {
            try
            {
                string filename = Param.UserCameraID + ".pfs";

                camera.Parameters.Save(filename, ParameterPath.CameraDevice);
                Log.Set(string.Format("Saving Parameters to file  {0}", filename));
            }
            catch (Exception e)
            {
                Log.Set(e.Message);
            }
        }

        private void Parameter_Load()
        {
            try
            {
                string filename = Param.UserCameraID + ".pfs";

                Log.Set(string.Format("Reading Parameters from file  {0}", filename));
                camera.Parameters.Load(filename, ParameterPath.CameraDevice);

                RaisePropertyChanged("Param");
            }
            catch (Exception e)
            {
                Log.Set(e.Message);
            }
        }

        private void Image_Save()
        {
            if (CameraImage == null)
            {
                Log.Set("이미지를 찾을 수 없습니다.");
                return;
            }

            try
            {
                DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\SaveImages" + 
                    DateTime.Now.ToString("_yyyy_MM_dd"));
                if (!di.Exists) 
                { 
                    di.Create(); 
                }

                string filename = Param.UserCameraID + DateTime.Now.ToString("_HH_mm_ss_fff") + ".png";

                using (var fileStream = new FileStream(di.FullName + @"\" + filename, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(CameraImage));
                    encoder.Save(fileStream);
                }

                Log.Set(string.Format("Image saved: {0}", filename));
            }
            catch (Exception e)
            {
                Log.Set(e.Message);
            }
        }
        #endregion Private Methods - Buttons

        #region Private Methods - Camera Event Callback
        // Occurs when a device with an opened connection is removed.
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            DestroyCamera();

            TagText = "";

            Log.Set("Camera Connection is removed.");

            EnShotOnce = false;
            EnShotContinuous = false;
            EnShotStop = false;
            EnResetCamera = false;
            EnConnect = true;
            EnDisConnet = false;
            EnParameterSave = false;
            EnParameterLoad = false;
            EnImageSave = false;
            EnImageSaveAuto = false;

            EnParamAll = false;
        }

        // Occurs when the connection to a camera device is opened.
        private void OnCameraOpened(Object sender, EventArgs e)
        {
            // Tag to TagText
            TagText = "";
            foreach (KeyValuePair<string, string> kvp in Tag)
                TagText += kvp.Key + ": " + kvp.Value + "\n";

            EnShotOnce = true;
            EnShotContinuous = true;
            EnShotStop = false;
            EnResetCamera = true;
            EnConnect = false;
            EnDisConnet = true;
            EnParameterSave = true;
            EnParameterLoad = true;
            EnImageSave = true;
            EnImageSaveAuto = true;

            Log.Clear();
            Log.Set("Camera Connection is opened.");
        }

        // Occurs when the connection to a camera device is closed.
        private void OnCameraClosed(Object sender, EventArgs e)
        {
            TagText = "";

            Log.Set("Camera Connection is closed.");

            EnShotOnce = false;
            EnShotContinuous = false;
            EnShotStop = false;
            EnResetCamera = false;
            EnConnect = true;
            EnDisConnet = false;
            EnParameterSave = false;
            EnParameterLoad = false;
            EnImageSave = false;
            EnImageSaveAuto = false;

            EnParamAll = false;
        }

        // Occurs when a camera starts grabbing.
        private void OnGrabStarted(Object sender, EventArgs e)
        {
            Log.Set("Camera start grabbing.");

            EnShotOnce = false;
            EnShotContinuous = false;
            EnShotStop = true;
            EnResetCamera = false;
            EnConnect = false;
            EnDisConnet = true;
            EnParameterSave = false;
            EnParameterLoad = false;
            EnImageSave = false;
            EnImageSaveAuto = false;

            EnParamAll = false;
        }

        // Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.GrabSucceeded)
                {
                    Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                    // Lock the bits of the bitmap.
                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    // Place the pointer to the buffer of the bitmap.
                    converter.OutputPixelFormat = PixelType.BGRA8packed;
                    IntPtr ptrBmp = bmpData.Scan0;
                    converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                    bitmap.UnlockBits(bmpData);

                    //UI thread에 접근하기 위해 dispatcher 사용
                    dispatcher.BeginInvoke((Action)(() =>
                    {
                        CameraImage = ConvertImage.BitmapToImageSource(bitmap);
                    }));

                    FrameRate = Param.ResultingFrameRateAbs;

                    if (IsImageSaveAuto == true)
                    {
                        Image_Save();
                    }

                    /*
                    dispatcher.BeginInvoke((Action)(() =>
                    {
                        CameraImageComplete();
                    }));
                    */
                }
            }
            catch (Exception exception)
            {
                Log.Set(string.Format("Exception: {0}", exception.Message));
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }

        // Occurs when a camera has stopped grabbing.
        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            if (e.Reason != GrabStopReason.UserRequest)
            {
                Log.Set(string.Format("A grab error occured:\n" + e.ErrorMessage));
            }
            else
            {
                Log.Set("Camera has stopped grabbing.");
            }

            EnShotOnce = true;
            EnShotContinuous = true;
            EnShotStop = false;
            EnResetCamera = true;
            EnConnect = false;
            EnDisConnet = true;
            EnParameterSave = true;
            EnParameterLoad = true;
            EnImageSave = true;
            EnImageSaveAuto = true;

            EnParamAll = true;
        }
        #endregion

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            DestroyCamera();
        }

        private void DestroyCamera()
        {
            try
            {
                if (camera != null)
                {
                    camera.StreamGrabber.Stop();

                    EnParamAll = false;
                    Param = null;
                }
            }
            catch (Exception e)
            {
                Log.Set(string.Format("Exception: {0}", e.Message));
            }

            // Destroy the camera object.
            try
            {
                if (camera != null)
                {
                    camera.Close();
                    camera.Dispose();
                    camera = null;
                }
            }
            catch (Exception e)
            {
                Log.Set(string.Format("Exception: {0}", e.Message));
            }

            // Destroy the converter object.
            if (converter != null)
            {
                converter.Dispose();
                converter = null;
            }
        }

        private void CameraImageComplete()
        {
            try
            {
                // 1) Create Process Info
                var psi = new ProcessStartInfo();
                psi.FileName = "D:\\Python\\Python37\\python.exe";

                // 2) Provide script and argument
                var script = @"D:\\Project\\Vision\\Vision_wpf\\Vision.Python\\Add.py";
                int aa = 3012;
                int bb = 2345;

                psi.Arguments = string.Format("{0} {1} {2}", script, aa, bb);

                // 3) Process configuration
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;

                // 4) Execute process and get output
                var errors = "";
                var results = "";

                using (var process = Process.Start(psi))
                {
                    errors = process.StandardError.ReadToEnd();
                    results = process.StandardOutput.ReadToEnd();
                }

                // 5) Display Output
                Debug.WriteLine("ERRORS : ");
                Debug.WriteLine(errors);
                Debug.WriteLine("");
                Debug.WriteLine("Results : ");
                Debug.WriteLine(results);
            }
            catch (Exception e)
            {
                Log.Set(string.Format("Exception: {0}", e.Message));
            }
        }

    }
}
