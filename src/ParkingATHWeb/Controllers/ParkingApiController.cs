﻿using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.ApiModels.Parking;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;

namespace ParkingATHWeb.Controllers
{
    [Route("api/Parking")]
    [ApiHeaderAuthorize]
    public class ParkingApiController : BaseApiController
    {
        private readonly IUserService _userService;

        public ParkingApiController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("RefreshCharges")]
        [HttpPost]
        public async Task<SmartJsonResult<int>> RefreshCharges([FromBody] RefreshChargesApiModel model)
        {
            if (!ModelState.IsValid)
                return SmartJsonResult<int>.Failure(GetModelStateErrors(ModelState));

            var getChargesResult = await _userService.GetChargesAsync(model.Email, GetHashFromHeader());

            return getChargesResult.IsValid
                ? SmartJsonResult<int>.Success(getChargesResult.Result)
                : SmartJsonResult<int>.Failure(getChargesResult.ValidationErrors);
        }

        [Route("OpenGate")]
        [HttpPost]
        public async Task<SmartJsonResult<int?>> OpenGate([FromBody] OpenGateApiModel model)
        {
            if (!ModelState.IsValid)
                return SmartJsonResult<int?>.Failure(GetModelStateErrors(ModelState));

            var openGateResult = await _userService.OpenGateAsync(model.Email, GetHashFromHeader());

            return openGateResult.IsValid
                ? SmartJsonResult<int?>.Success(openGateResult.Result)
                : SmartJsonResult<int?>.Failure(openGateResult.ValidationErrors);
        }
    }
}
