using System;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IImageProcessorService : IDependencyService
    {
        ServiceResult<byte[], Guid> ProcessAndSaveImage(byte[] source, string path);
        void DeleteImagesByPath(string path);
    }
}
