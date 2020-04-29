using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Basler.Pylon;

using CAMERA_VISION.ModuleCamera.Views;
using CAMERA_VISION.ModuleCamera.Models;
using CAMERA_VISION.ModuleCamera.ViewModels;

using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace CAMERA_VISION.ModuleCamera
{
    public class ModuleCam : IModule
    {
        private ObservableCollection<CAM> _cams = new ObservableCollection<CAM>();
        public ObservableCollection<CAM> CAMs
        {
            get { return _cams; }
            set { _cams = value; }
        }

        List<ICameraInfo> BaslerCameraAll = new List<ICameraInfo>();

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            IRegion region = regionManager.Regions["CameraTabRegion"];

            // Basler 카메라 검색
            UpdateBaslerDeviceList();

            // 추후 다른 카메라 추가되면 검색 메소드 추가할 자리

            // Create TabControl
            if (CAMs.Count == 0)
            {
                var tab = containerProvider.Resolve<ViewNone>();
                (tab.DataContext as ViewNoneViewModel).Name = "None";
                region.Add(tab);
                region.Activate(tab);
            }
            else
            {
                foreach (var cam in CAMs)
                {
                    if (cam.CAMType == CAMTYPE.BASLER)
                    {
                        var tab = containerProvider.Resolve<ViewBasler>();
                        (tab.DataContext as ViewBaslerViewModel).Name = cam.Name;
                        (tab.DataContext as ViewBaslerViewModel).Tag = (ICameraInfo)cam.Tag;
                        region.Add(tab);
                        region.Activate(tab);
                    }
                }
            }
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        private void UpdateBaslerDeviceList()
        {
            try
            {
                // Ask the camera finder for a list of camera devices.
                BaslerCameraAll = CameraFinder.Enumerate();

                foreach (ICameraInfo cameraInfo in BaslerCameraAll)
                {
                    // Loop over all cameras in the list of cameras.
                    bool newitem = true;
                    foreach (CAM cam in CAMs)
                    {
                        ICameraInfo tag = cam.Tag as ICameraInfo;

                        // Is the camera found already in the list of cameras?
                        if (tag[CameraInfoKey.FullName] == cameraInfo[CameraInfoKey.FullName])
                        {
                            tag = cameraInfo;
                            newitem = false;
                            break;
                        }
                    }

                    // If the camera is not in the list, add it to the list.
                    if (newitem)
                    {
                        // Create the item to display.
                        CAM cam = new CAM()
                        {
                            Name = cameraInfo[CameraInfoKey.FullName],
                            Tag = cameraInfo,
                            CAMType = CAMTYPE.BASLER
                        };

                        CAMs.Add(cam);
                    }
                }

                // Remove old camera devices that have been disconnected.
                foreach (CAM cam in CAMs)
                {
                    bool exists = false;

                    // For each camera in the list, check whether it can be found by enumeration.
                    foreach (ICameraInfo cameraInfo in BaslerCameraAll)
                    {
                        if (((ICameraInfo)cam.Tag)[CameraInfoKey.FullName] == cameraInfo[CameraInfoKey.FullName])
                        {
                            exists = true;
                            break;
                        }
                    }
                    // If the camera has not been found, remove it from the list view.
                    if (!exists)
                    {
                        CAMs.Remove(cam);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: {0}", e.Message);
            }
        }
    }
}