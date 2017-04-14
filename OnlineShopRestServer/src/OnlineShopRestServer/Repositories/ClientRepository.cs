using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using OnlineShopRestServer.Models;

namespace OnlineShopRestServer.Repositories
{
    public class ClientRepository
    {
        ShopContext db;

        public ClientRepository(ShopContext context)
        {
            this.db = context;
        }

        public IEnumerable<client> SelectAll()
        {
            return db.client;
        }
        
        public client Select(int id)
        {
            return (client)db.client.Where(client => client.id == id);
        }
        public IEnumerable<client> SelectByFilter(List<KeyValuePair<string, StringValues>> filter)
        { 
            return FilterSelect<client>.GetWhere(filter, db);
        }

        public client Insert(client item)
        {
            db.client.Add(item);
            Save();
            return item;
        }

        public client Update(client item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }

        public int Delete(int id)
        {
            client item = db.client.Find(id);
            if (item != null)
                db.client.Remove(item);
            Save();
            return id;
        }
        private void Save()
        {
            db.SaveChanges();
        }
    }
}
