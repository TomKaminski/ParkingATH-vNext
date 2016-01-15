using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;

namespace ParkingATHWeb.Controllers
{
    [Route("api/Test")]
    public class TestApiController : BaseApiController
    {
        private readonly IUserService _userService;

        public TestApiController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [ApiHeaderAuthorize]
        [HttpGet]
        public async Task<ApiResult<UserBaseDto>> GetStudentDataWithHeader(string userName)
        {
            var student = await _userService.GetByEmailAsync(userName);
            return ApiResult<UserBaseDto>.Success(student.Result);
        }

        [HttpGet]
        public async Task<ApiResult<UserBaseDto>> GetStudentDataWithoutHeader(string userName)
        {
            var student = await _userService.GetByEmailAsync(userName);
            return ApiResult<UserBaseDto>.Success(student.Result);
        }

        [HttpGet]
        [ApiHeaderAuthorize]
        public async Task<ApiResult<bool>> CheckUserHeader(string userName)
        {
            var checkHeaderHash = await _userService.CheckHashAsync(userName, GetHashFromHeader());
            return ApiResult<bool>.Success(checkHeaderHash.Result);
        }
    }
}
