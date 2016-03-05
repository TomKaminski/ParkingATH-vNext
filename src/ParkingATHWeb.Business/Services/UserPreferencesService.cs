using System;
using System.Threading.Tasks;
using AutoMapper;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.Business.Services
{
    public class UserPreferenesService:EntityService<UserPreferencesDto,UserPreferences, int>, IUserPreferencesService
    {
        private readonly IUserPreferencesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageProcessorService _imageProcessorService;
        private const string PlaceholderPhotoName = "avatar-placeholder";

        public UserPreferenesService(IUserPreferencesRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IImageProcessorService imageProcessorService) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _imageProcessorService = imageProcessorService;
        }

        public async Task<ServiceResult<Guid>> SetUserAvatarAsync(byte[] sourceImage, int userId, string folderPath)
        {
            var imageProcessorJob = _imageProcessorService.ProcessAndSaveImage(sourceImage, folderPath);
            var userPreference = await _repository.SingleOrDefaultAsync(x => x.UserId == userId);
            if (userPreference.ProfilePhotoId != null)
            {
                _imageProcessorService.DeleteImagesByPath(folderPath+userPreference.ProfilePhotoId);
            }
            userPreference.ProfilePhoto = imageProcessorJob.Result;
            userPreference.ProfilePhotoId = imageProcessorJob.SecondResult;
            _repository.Edit(userPreference);
            await _unitOfWork.CommitAsync();
            return ServiceResult<Guid>.Success(imageProcessorJob.SecondResult);
        }

        public async Task<ServiceResult<string>> DeleteProfilePhotoAsync(int userId, string folderPath)
        {
            var userPreference = await _repository.SingleOrDefaultAsync(x => x.UserId == userId);
            _imageProcessorService.DeleteImagesByPath(folderPath + userPreference.ProfilePhotoId);

            userPreference.ProfilePhoto = null;
            userPreference.ProfilePhotoId = null;
            _repository.Edit(userPreference);
            await _unitOfWork.CommitAsync();
            return ServiceResult<string>.Success(PlaceholderPhotoName);
        }
    }
}
