using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Npgsql;

namespace CreditCard.Snapshot
{
    public class CreditCardReports
    {
        private readonly string _connectionString = "Host=localhost;Database=aggregates;Username=aggregates;Password=password";

        public async Task<IEnumerable<CreditCardLimit>> Generate()
        {
            var data = new List<CreditCardLimit>();
            const string sqlQuery = @"
            select
                cc.""AvaliableLimit"" ""Limit"",
                ao.""Name"" ""Owner""
            from
                ""snapshot"".""CreditCards"" cc
            join ""snapshot"".""AccountOwners"" ao on
                cc.""OwnerId"" = ao.""Id""";

            using(var db = new NpgsqlConnection(_connectionString))
            {
                data = (await db.QueryAsync<CreditCardLimit>(sqlQuery)).ToList();
            }

            return data;
        }
    }
}
