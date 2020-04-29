using System;
using System.Collections.Generic;
using System.Text;

namespace CAMERA_VISION.Core.Contracts.Service
{
    public interface IFileService
    {
        T Read<T>(string folderPath, string fileName);

        void Save<T>(string folderPath, string fileName, T content);

        void Delete(string folderPath, string fileName);
    }
}
