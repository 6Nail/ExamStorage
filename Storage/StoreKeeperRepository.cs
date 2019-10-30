using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Storage
{
    class StoreKeeperRepository
    {
        private readonly string connectionString;
        public StoreKeeperRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Add(StoreKeeper storeKeeper)
        {
            string sql = "Insert into StoreKeepers(@Id,@Name);";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Insert(storeKeeper);
            }
        }
        public void Update(StoreKeeper storeKeeper)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Update(storeKeeper);
            }
        }

        public void Delete(StoreKeeper storeKeeper)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Delete(storeKeeper);
            }
        }
        public ICollection<StoreKeeper> GetStoreKeeper()
        {
            string sql = "Select * From StoreKeepers;";
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<StoreKeeper>(sql).ToList();
            }
        }
    }
}
