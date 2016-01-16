using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.ApiModels.Test;
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
        [HttpPost]
        [Route("GetStudentDataWithHeader")]
        public async Task<ApiResult<UserBaseDto>> GetStudentDataWithHeader([FromBody]GetStudentDataHeaderApiModel model)
        {
            var student = await _userService.GetByEmailAsync(model.userName);
            return ApiResult<UserBaseDto>.Success(student.Result);
        }


        [HttpGet]
        [Route("Test")]
        public ApiResult<bool> Test()
        {
            return ApiResult<bool>.Success(true);
        }

        [HttpPost]
        [Route("GetStudentDataWithoutHeader")]
        public async Task<ApiResult<UserBaseDto>> GetStudentDataWithoutHeader([FromBody]GetStudentDataHeaderApiModel model)
        {
            var student = await _userService.GetByEmailAsync(model.userName);
            return ApiResult<UserBaseDto>.Success(student.Result);
        }

        [HttpPost]
        [ApiHeaderAuthorize]
        [Route("CheckUserHeader")]
        public async Task<ApiResult<bool>> CheckUserHeaderr([FromBody]GetStudentDataHeaderApiModel model)
        {
            var checkHeaderHash = await _userService.CheckHashAsync(model.userName, GetHashFromHeader());
            return ApiResult<bool>.Success(checkHeaderHash.Result);
        }
    }
}
