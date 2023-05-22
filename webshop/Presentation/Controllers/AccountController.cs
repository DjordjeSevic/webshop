using Contracts.Dtos.Errors;
using Contracts.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AppUserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _serviceManager.UserService.GetCurrentUser(email);
            user.Token = _serviceManager.TokenService.CreateToken(user);

            return user;
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _serviceManager.UserService.CheckEmailExists(email);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return await _serviceManager.UserService.GetUserAddress(email);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.UserService.UpdateUserAddress(email, addressDto);

            if (result != null) return Ok(addressDto);

            return BadRequest("Problem updating the user");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManager.UserService.Login(loginDto);

            if (user == null) return Unauthorized(new ApiResponse(401));

            user.Token = _serviceManager.TokenService.CreateToken(user);

            return user;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUserDto>> Register(RegisterDto registerDto)
        {
            var exists = await _serviceManager.UserService.CheckEmailExists(registerDto.Email);

            if (exists) return BadRequest(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" }, StatusCode=400});

            var user = await _serviceManager.UserService.Register(registerDto);

            if (user == null) return BadRequest(new ApiResponse(400));

            user.Token = _serviceManager.TokenService.CreateToken(user);

            return user;
        }
    }
}
