using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Storage
{
    public class WayBillRepository
    {
        private readonly string connectionString;
        public WayBillRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void AddWayBill(Waybill doki)
        {
            string sql = "Insert into WayBills (@id,@Quantity,@Price,@StoreKeeperId,@ProductId,@DeliveryDate,@DepartDate);";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Insert(doki);
            }
        }
        public void UpdateWayBill(Waybill doki)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Update(doki);
            }
        }

        public void Delete(Waybill waybill)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Delete(waybill);
            }
        }
        public ICollection<Waybill> GetWayBill()
        {
            string sql = "Select * From WayBills;";
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Waybill>(sql).ToList();
            }
        }
       
    }
    
}
