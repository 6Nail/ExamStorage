using System;
using System.Collections.Generic;
using System.Text;

namespace Storage
{
    public class Waybill
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Quantity { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime DepartDate { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public int StoreKeeperId { get; set; }
        public string Provider { get; set; }
        public string Receiver { get; set; }
        public bool IsExport { get; set; }


    }
}
