using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CAMERA_VISION.ModuleCamera.Models
{
    public class BaslerLog : BindableBase
    {
        private readonly object _lockLogData = new object();
        private ObservableCollection<BaslerLogData> _BaslerLogDatas = new ObservableCollection<BaslerLogData>();
        public ObservableCollection<BaslerLogData> BaslerLogDatas
        {
            get { return _BaslerLogDatas; }
            set
            {
                _BaslerLogDatas = value;
                RaisePropertyChanged("BaslerLogDatas");
            }
        }

        public void Set(string logtext)
        {
            BaslerLogDatas.Add(new BaslerLogData(DateTime.Now, logtext));

            if (BaslerLogDatas.Count >= 50)
            {
                BaslerLogDatas.RemoveAt(0);
            }
        }

        public void Clear()
        {
            BaslerLogDatas.Clear();
        }

        public BaslerLog()
        {
            BindingOperations.EnableCollectionSynchronization(BaslerLogDatas, _lockLogData);
            BaslerLogDatas.Clear();
        }

    }


    public class BaslerLogData
    {
        private string _LogTime;
        public string LogTime
        {
            get { return _LogTime; }
            set { _LogTime = value; }
        }

        private string _LogText;
        public string LogText
        {
            get { return _LogText; }
            set { _LogText = value; }
        }

        public BaslerLogData(DateTime logTime, string logText)
        {
            this.LogTime = logTime.ToString("HH:mm:ss.fff");
            this.LogText = logText;
        }
    }
}
