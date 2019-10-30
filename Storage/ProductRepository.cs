using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Storage
{
    class ProductRepository
    {
        private readonly string connectionString;
        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void AddProduct(Product product)
        {
            
            string sql = "Insert into Products (@Name, @Quantity,@Price);";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Insert(product);
            }
        }
        public void UpdateWayBill(Product product)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Update(product);
            }
        }

        public void Delete(Product product)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Delete(product);
            }
        }
        public ICollection<Product> GetProduct()
        {
            string sql = "Select * From Products;";
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Product>(sql).ToList();
            }
        }
    }
}
