using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using OnlineShopRestServer.Models;

namespace OnlineShopRestServer.Repositories
{
    public class AddressRepository 
    {
        ShopContext db;

        public AddressRepository(ShopContext context)
        {
            this.db = context;
        }

        public IEnumerable<address> SelectAll()
        {
            return db.address;
        }

        public address Select(int id)
        {
            return (address)db.address.Where(address => address.id == id);
        }
        public IEnumerable<address> SelectByFilter(List<KeyValuePair<string, StringValues>> filter)
        {
            return FilterSelect<address>.GetWhere(filter, db);
        }

        public address Insert(address item)
        {
            db.address.Add(item);
            Save();
            return item;
        }

        public address Update(address item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }

        public int Delete(int id)
        {
            address item = db.address.Find(id);
            if (item != null)
                db.address.Remove(item);
            Save();
            return id;
        }
        private void Save()
        {
            db.SaveChanges();
        }

    }
}
