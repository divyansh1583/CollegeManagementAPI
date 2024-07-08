using CollegeManagementAPI.Domain.Entities;

namespace CollegeManagementAPI.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserDetail userDetail);
        Task<IEnumerable<UserDetail>> GetUsersAsync();
    }


}
