using System;
using System.IO;
using ImageResizer;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Business.Services
{
    public class ImageProcessorService : IImageProcessorService
    {
        public ServiceResult<byte[], Guid> ProcessAndSaveImage(byte[] source, string path)
        {
            var guid = Guid.NewGuid();
            var imageJob = new ImageJob(source, path + guid, new Instructions("maxwidth=300&maxheight=300&format=png"))
            {
                CreateParentDirectory = true,
                AddFileExtension = true,
            };
            imageJob.Build();
            return ServiceResult<byte[], Guid>.Success(File.ReadAllBytes(imageJob.FinalPath), guid);
        }

        public void DeleteImagesByPath(string path)
        {
            if (File.Exists($"{path}.png"))
                File.Delete($"{path}.png");
        }
    }
}
