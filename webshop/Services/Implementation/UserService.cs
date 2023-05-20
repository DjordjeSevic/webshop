using AutoMapper;
using Contracts.Dtos.Identity;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.Implementation
{
    internal class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManger;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManger, IMapper mapper)
        {
            _userManager = userManager;
            _signInManger = signInManger;
            _mapper = mapper;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<AddressDto> GetUserAddress(string email)
        {
            var user = await _userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

            return new AddressDto
            {
                State = user.Address.State,
                City = user.Address.City,
                Street = user.Address.Street,
                ZipCode = user.Address.ZipCode
            };
        }

        public async Task<AddressDto> UpdateUserAddress(string email, AddressDto addressDto)
        {
            var user = await _userManager.FindByEmailAsync(email);

            user.Address = new Address
            {
                State = addressDto.State,
                Street = addressDto.Street,
                City = addressDto.City,
                ZipCode = addressDto.ZipCode
            };

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return null;

            return addressDto;
        }

        public async Task<AppUserDto> GetCurrentUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var role = (await _userManager.GetRolesAsync(user)).ToList().First();

            return new AppUserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Role = role
            };
        }

        public async Task<AppUserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return null;

            var role = (await _userManager.GetRolesAsync(user)).ToList().First();

            var result = await _signInManger.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return null;

            return new AppUserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Role = role
            };
        }

        public async Task<AppUserDto> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return null;

            var resultRole = await _userManager.AddToRoleAsync(user, "Customer");

            return new AppUserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Role = "Customer"
            };
        }
    }
}
