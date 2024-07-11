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
        //loging use if email id and 
        public async Task<Result> LoginUserAsync(LoginDetails loginDetails)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDetails.Email);

            if (user == null || user.Password != loginDetails.Password)
            {
                return new Result { IsSuccess = false, Message = "Invalid email or password" };
            }
            return new Result { IsSuccess = true, Message = "Login successful" };
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
