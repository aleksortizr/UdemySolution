using Dapper;
using Northwind.Models;
using Northwind.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Northwind.DataAccess
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(string connectionString):base(connectionString)
        {
        }

        /// <summary>
        /// Gets the page of Supplier elements
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="rows">page size</param>
        /// <returns></returns>
        public IEnumerable<Supplier> SupplierPagedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Supplier>("dbo.SupplierPagedList",
                        parameters,
                        commandType: CommandType.StoredProcedure);
        }
    }
}
