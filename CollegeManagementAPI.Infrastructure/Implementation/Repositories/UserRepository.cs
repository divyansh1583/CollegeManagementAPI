using Dapper;
using CollegeManagementAPI.Application.Interfaces.Repositories;
using CollegeManagementAPI.Domain.Entities;
using CollegeManagementAPI.Infrastructure.Data;
using System.Threading.Tasks;
using System.Data;

namespace CollegeManagementAPI.Infrastructure.Implementation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        //private readonly DbService _dbService;


        public UserRepository(
            DapperContext context
            //DbService dbService
            )
        {
            _context = context;
            //_dbService = dbService;
        }

        // Getting users list
        public async Task<IEnumerable<UserDetail>> GetUsersAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "exec dc_getuserdetails";

                return await connection.QueryAsync<UserDetail>(query);
            }
            //return await _dbService.QueryAsync("EXEC DC_GetUserDetails");

        }
        //getting user by email
        public async Task<UserDetail> GetUserByEmailAsync(string email)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM DC_LoginCredentials lc WHERE Email=@Email";

                var result = await connection.QueryAsync<UserDetail>(query, new { Email = email });

                return result.FirstOrDefault();
            }
        }

        // Running DC_InsertUserAndLoginCredentials Stored Procedure
        public async Task<int> InsertUserAndLoginCredentials(UserDetail userDetail)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "EXEC DC_InsertUserAndLoginCredentials @FirstName, @LastName, @Email, @PhoneNumber, @CountryId, @StateId, @Gender, @Password";

                var parameters = new
                {
                    userDetail.FirstName,
                    userDetail.LastName,
                    userDetail.Email,
                    userDetail.PhoneNumber,
                    userDetail.CountryId,
                    userDetail.StateId,
                    userDetail.Gender,
                    userDetail.Password
                };

                return await connection.ExecuteAsync(query, parameters);
            }
        }

        // Update user
        public async Task<int> UpdateUserAsync(UserDetail userDetail)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "EXEC DC_UpdateUserAndLoginCredentials @UserId, @FirstName, @LastName, @Email, @PhoneNumber, @CountryId, @StateId, @Gender, @Password";

                var parameters = new
                {
                    userDetail.UserId,
                    userDetail.FirstName,
                    userDetail.LastName,
                    userDetail.Email,
                    userDetail.PhoneNumber,
                    userDetail.CountryId,
                    userDetail.StateId,
                    userDetail.Gender,
                    userDetail.Password
                };

                return await connection.ExecuteAsync(query, parameters);
            }
        }

        // Delete user
        public async Task<int> DeleteUserAsync(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "EXEC DC_DeleteUserAndLoginCredentials @UserId";

                var parameters = new { UserId = id };

                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}