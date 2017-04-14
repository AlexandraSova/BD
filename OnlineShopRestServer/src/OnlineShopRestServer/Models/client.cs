using System;

namespace OnlineShopRestServer.Models
{
    public class client 
    {
        public int id { get; set; }
        public string fio { get; set; }
        public DateTime birthday { get; set; }
        public string phone { get; set; }
        public int manager_id { get; set; }
        public int address_id { get; set; }

     
    }
}
