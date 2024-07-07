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

        public async Task<int> RegisterUser(UserDetail userDetail)
        {
            // Additional business logic can be added here if needed
            return await _userRepository.InsertUserAndLoginCredentials(userDetail);
        }
    }


}
