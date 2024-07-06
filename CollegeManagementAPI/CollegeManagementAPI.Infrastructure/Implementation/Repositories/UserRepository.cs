using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeManagementAPI.Application.Interfaces.Repositories;
using CollegeManagementAPI.Domain.Entities;
using CollegeManagementAPI.Infrastructure.Data;
using Dapper;

namespace CollegeManagementAPI.Infrastructure.Implementation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<UserDetail> GetUserByEmailAsync(string email)
        {
            var query = "SELECT * FROM DC_UserDetail WHERE Email = @Email";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<UserDetail>(query, new { Email = email });
            }
        }

        public async Task AddUserAsync(UserDetail user)
        {
            var query = "INSERT INTO DC_UserDetail (FirstName, LastName, Email, PhoneNumber, CountryId, StateId, Gender, IsActive, IsDeleted) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @CountryId, @StateId, @Gender, @IsActive, @IsDeleted); SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = _context.CreateConnection())
            {
                user.UserId = await connection.QuerySingleAsync<int>(query, user);
            }
        }

        public async Task AddLoginCredentialAsync(LoginCredential credential)
        {
            var query = "INSERT INTO DC_LoginCredentials (UserId, Email, Password, IsActive, IsDeleted) VALUES (@UserId, @Email, @Password, @IsActive, @IsDeleted)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, credential);
            }
        }
    }


}
