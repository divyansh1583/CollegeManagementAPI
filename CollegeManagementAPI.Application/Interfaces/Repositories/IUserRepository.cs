using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeManagementAPI.Domain.Entities;

namespace CollegeManagementAPI.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserDetail> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserDetail user);
        Task AddLoginCredentialAsync(LoginCredential credential);
    }

}
