using Basler.Pylon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAMERA_VISION.ModuleCamera.Models
{
    public class CAM
    {
        #region Properties

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private ICameraInfo _Tag;
        public ICameraInfo Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        private CAMTYPE _CAMType;
        public CAMTYPE CAMType
        {
            get { return _CAMType; }
            set { _CAMType = value; }
        }

        #endregion
    }
}
