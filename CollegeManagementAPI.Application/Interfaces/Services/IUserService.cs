using CollegeManagementAPI.Application.DTOs;
using CollegeManagementAPI.Domain.Entities;

namespace CollegeManagementAPI.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDetail>> GetUsersAsync();
        Task<Result> LoginUserAsync(LoginDetails loginDetails);
        Task<int> RegisterUser(UserDetail userDetail);
        Task<int> UpdateUser(UserDetail userDetail);
        Task<int> DeleteUser(int userId);

    }


}
