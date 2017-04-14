using System;

namespace OnlineShopRestServer.Models
{
    public class order
    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public DateTime finish { get; set; }
        public int manager_id { get; set; }
        public int client_id { get; set; }

 
    }
}
