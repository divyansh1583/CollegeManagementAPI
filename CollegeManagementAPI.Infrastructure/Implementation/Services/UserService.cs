using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeManagementAPI.Application.DTOs;
using CollegeManagementAPI.Application.Interfaces.Repositories;
using CollegeManagementAPI.Application.Interfaces.Services;
using CollegeManagementAPI.Domain.Entities;

namespace CollegeManagementAPI.Infrastructure.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var userDetail = new UserDetail
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                CountryId = userDto.CountryId,
                StateId = userDto.StateId,
                Gender = userDto.Gender,
                IsDeleted = false
            };

            await _userRepository.AddUserAsync(userDetail);

            var loginCredential = new LoginCredential
            {
                UserId = userDetail.UserId,
                Email = userDto.Email,
                Password = userDto.Password,
                IsActive = true,
            };

            await _userRepository.AddLoginCredentialAsync(loginCredential);
        }
    }

}
