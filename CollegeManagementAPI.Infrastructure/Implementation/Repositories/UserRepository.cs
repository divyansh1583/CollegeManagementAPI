﻿using Dapper;
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

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> InsertUserAndLoginCredentials(UserDetail userDetail)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "InsertUserAndLoginCredentials";

                var parameters = new
                {
                    userDetail.FirstName,
                    userDetail.LastName,
                    userDetail.Email,
                    userDetail.PhoneNumber,
                    userDetail.CountryId,
                    userDetail.StateId,
                    userDetail.Gender,
                    Password = userDetail.Password // Assuming Password is added to UserDetail class
                };

                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
