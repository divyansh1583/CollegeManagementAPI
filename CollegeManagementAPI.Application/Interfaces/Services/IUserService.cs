using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeManagementAPI.Application.DTOs;

namespace CollegeManagementAPI.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task AddUserAsync(UserDto userDto);
    }

}
