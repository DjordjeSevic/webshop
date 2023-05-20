using Contracts.Dtos.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<bool> CheckEmailExists(string email);
        Task<AddressDto> GetUserAddress(string email);
        Task<AddressDto> UpdateUserAddress(string email, AddressDto addressDto);
        Task<AppUserDto> GetCurrentUser(string email);
        Task<AppUserDto> Login(LoginDto loginDto);
        Task<AppUserDto> Register(RegisterDto registerDto);
    }
}
