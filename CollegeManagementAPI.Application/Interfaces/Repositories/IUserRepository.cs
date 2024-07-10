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
        Task<IEnumerable<UserDetail>> GetUsersAsync();
        Task<int> InsertUserAndLoginCredentials(UserDetail userDetail);
        Task<UserDetail> GetUserByEmailAsync(string email);
        Task<int> UpdateUserAsync(UserDetail userDetail);
        Task<int> DeleteUserAsync(int id);
    }


}
