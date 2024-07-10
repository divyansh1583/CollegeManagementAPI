using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagementAPI.Infrastructure.Data
{
    public class DbService
    {
        private readonly DapperContext _context;

        public DbService(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                return await connection.QueryAsync<T>(query, parameters).ConfigureAwait(false);
            }
        }
        public async Task<IEnumerable<T>> ExecuteAsync<T>(string query, object parameters)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                return await connection.QueryAsync<T>(query, parameters);
            }
        }
    }
}
