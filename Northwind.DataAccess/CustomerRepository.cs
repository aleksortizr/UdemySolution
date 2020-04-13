using Dapper;
using Northwind.Models;
using Northwind.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Northwind.DataAccess
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<Customer> CustomerPagedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Customer>("dbo.CustomerPagedList",
                        parameters,
                        commandType: CommandType.StoredProcedure);
        }
    }
}
