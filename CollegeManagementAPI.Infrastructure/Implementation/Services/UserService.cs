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

        //Get
        public async Task<IEnumerable<UserDetail>> GetUsersAsync()
        {

            return await _userRepository.GetUsersAsync();
        }

        //Insert
        public async Task<int> RegisterUser(UserDetail userDetail)
        {

            return await _userRepository.InsertUserAndLoginCredentials(userDetail);
        }

        //Update
        public async Task<int> UpdateUser(UserDetail userDetail)
        {
            return await _userRepository.UpdateUserAsync(userDetail);
        }

        //Delete
        public async Task<int> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

    }



}
