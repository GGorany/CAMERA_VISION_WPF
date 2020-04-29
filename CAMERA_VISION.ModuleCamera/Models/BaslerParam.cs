using Basler.Pylon;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAMERA_VISION.ModuleCamera.Models
{
    public class BaslerParam : BindableBase
    {
        private Camera camera;

        #region User Camera ID
        public string UserCameraID
        {
            get => camera.Parameters[PLCamera.DeviceUserID].GetValue();
            set { camera.Parameters[PLCamera.DeviceUserID].TrySetValue(value); }
        }
        #endregion

        #region Pixel Format
        public List<string> PixelFormats
        {
            get
            {
                IEnumerable<string> AllItems = camera.Parameters[PLCamera.PixelFormat].GetAllValues();
                List<string> SettableItems = new List<string>();

                foreach (string Item in AllItems)
                {
                    if (camera.Parameters[PLCamera.PixelFormat].CanSetValue(Item))
                    {
                        SettableItems.Add(Item);
                    }
                }

                return SettableItems;
            }
        }
        public string SelectedPinxelFormat
        {
            get => camera.Parameters[PLCamera.PixelFormat].GetValue();
            set
            {
                camera.Parameters[PLCamera.PixelFormat].TrySetValue(value);
                RaisePropertyChanged("Gain");
                RaisePropertyChanged("GainMax");
            }
        }
        #endregion

        #region Trigger Mode
        public List<string> TriggerModes
        {
            get
            {
                IEnumerable<string> AllItems = camera.Parameters[PLCamera.TriggerMode].GetAllValues();
                List<string> SettableItems = new List<string>();

                foreach (string Item in AllItems)
                {
                    if (camera.Parameters[PLCamera.TriggerMode].CanSetValue(Item))
                    {
                        SettableItems.Add(Item);
                    }
                }

                return SettableItems;
            }
        }
        public string SelectedTriggerMode
        {
            get => camera.Parameters[PLCamera.TriggerMode].GetValue();
            set { camera.Parameters[PLCamera.TriggerMode].TrySetValue(value); }
        }

        public List<string> TriggerSources
        {
            get
            {
                IEnumerable<string> AllItems = camera.Parameters[PLCamera.TriggerSource].GetAllValues();
                List<string> SettableItems = new List<string>();

                foreach (string Item in AllItems)
                {
                    if (camera.Parameters[PLCamera.TriggerSource].CanSetValue(Item))
                    {
                        SettableItems.Add(Item);
                    }
                }

                return SettableItems;
            }
        }
        public string SelectedTriggerSource
        {
            get => camera.Parameters[PLCamera.TriggerSource].GetValue();
            set { camera.Parameters[PLCamera.TriggerSource].TrySetValue(value); }
        }
        #endregion

        #region Width
        public int Width
        {
            get => (int)camera.Parameters[PLCamera.Width].GetValue();
            set
            {
                camera.Parameters[PLCamera.Width].TrySetValue(value, IntegerValueCorrection.Nearest);
                RaisePropertyChanged("OffsetX");
                RaisePropertyChanged("OffsetXMax");
            }
        }
        public int WidthMax
        {
            get => (int)camera.Parameters[PLCamera.Width].GetMaximum();
            set { camera.Parameters[PLCamera.Width].TrySetToMaximum(); }
        }
        public int WidthMin
        {
            get => (int)camera.Parameters[PLCamera.Width].GetMinimum();
            set { camera.Parameters[PLCamera.Width].TrySetToMinimum(); }
        }
        public int WidthInc { get => (int)camera.Parameters[PLCamera.Width].GetIncrement(); }
        #endregion

        #region Height
        public int Height
        {
            get { return (int)camera.Parameters[PLCamera.Height].GetValue(); }
            set
            {
                camera.Parameters[PLCamera.Height].TrySetValue(value, IntegerValueCorrection.Nearest);
                RaisePropertyChanged("OffsetY");
                RaisePropertyChanged("OffsetYMax");
            }
        }
        public int HeightMax
        {
            get => (int)camera.Parameters[PLCamera.Height].GetMaximum();
            set { camera.Parameters[PLCamera.Height].TrySetToMaximum(); }
        }
        public int HeightMin
        {
            get => (int)camera.Parameters[PLCamera.Height].GetMinimum();
            set { camera.Parameters[PLCamera.Height].TrySetToMinimum(); }
        }
        public int HeightInc { get => (int)camera.Parameters[PLCamera.Height].GetIncrement(); }
        #endregion

        #region Offset X
        public int OffsetX
        {
            get { return (int)camera.Parameters[PLCamera.OffsetX].GetValue(); }
            set
            {
                camera.Parameters[PLCamera.OffsetX].TrySetValue(value, IntegerValueCorrection.Nearest);
                RaisePropertyChanged("Width");
                RaisePropertyChanged("WidthMax");
            }
        }
        public int OffsetXMax
        {
            get => (int)camera.Parameters[PLCamera.OffsetX].GetMaximum();
            set { camera.Parameters[PLCamera.OffsetX].TrySetToMaximum(); }
        }
        public int OffsetXMin
        {
            get => (int)camera.Parameters[PLCamera.OffsetX].GetMinimum();
            set { camera.Parameters[PLCamera.OffsetX].TrySetToMinimum(); }
        }
        public int OffsetXInc { get => (int)camera.Parameters[PLCamera.OffsetX].GetIncrement(); }
        #endregion

        #region Offset Y
        public int OffsetY
        {
            get { return (int)camera.Parameters[PLCamera.OffsetY].GetValue(); }
            set
            {
                camera.Parameters[PLCamera.OffsetY].TrySetValue(value, IntegerValueCorrection.Nearest);
                RaisePropertyChanged("Height");
                RaisePropertyChanged("HeightMax");
            }
        }
        public int OffsetYMax
        {
            get => (int)camera.Parameters[PLCamera.OffsetY].GetMaximum();
            set { camera.Parameters[PLCamera.OffsetY].TrySetToMaximum(); }
        }
        public int OffsetYMin
        {
            get => (int)camera.Parameters[PLCamera.OffsetY].GetMinimum();
            set { camera.Parameters[PLCamera.OffsetY].TrySetToMinimum(); }
        }
        public int OffsetYInc { get => (int)camera.Parameters[PLCamera.OffsetY].GetIncrement(); }
        #endregion

        #region Gain
        public int Gain
        {
            get { return (int)camera.Parameters[PLCamera.GainRaw].GetValue(); }
            set { camera.Parameters[PLCamera.GainRaw].TrySetValue(value, IntegerValueCorrection.Nearest); }
        }
        public int GainMax
        {
            get => (int)camera.Parameters[PLCamera.GainRaw].GetMaximum();
            set { camera.Parameters[PLCamera.GainRaw].TrySetToMaximum(); }
        }
        public int GainMin
        {
            get => (int)camera.Parameters[PLCamera.GainRaw].GetMinimum();
            set { camera.Parameters[PLCamera.GainRaw].TrySetToMinimum(); }
        }
        public int GainInc { get => (int)camera.Parameters[PLCamera.GainRaw].GetIncrement(); }
        #endregion

        #region Exposure Time
        public int ExposureTime
        {
            get { return (int)camera.Parameters[PLCamera.ExposureTimeRaw].GetValue(); }
            set { camera.Parameters[PLCamera.ExposureTimeRaw].TrySetValue(value, IntegerValueCorrection.Nearest); }
        }
        public int ExposureTimeMax
        {
            get => (int)camera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
            set { camera.Parameters[PLCamera.ExposureTimeRaw].TrySetToMaximum(); }
        }
        public int ExposureTimeMin
        {
            get => (int)camera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
            set { camera.Parameters[PLCamera.ExposureTimeRaw].TrySetToMinimum(); }
        }
        public int ExposureTimeInc { get => (int)camera.Parameters[PLCamera.ExposureTimeRaw].GetIncrement(); }
        #endregion

        #region Frame Rate
        public double ResultingFrameRateAbs { get => (double)camera.Parameters[PLCamera.ResultingFrameRateAbs].GetValue(); }
        #endregion

        public void ResetDevice()
        {
            camera.Parameters[PLCamera.DeviceReset].TryExecute();
        }

        public BaslerParam(Camera camera)
        {
            this.camera = camera;
        }
    }
}
