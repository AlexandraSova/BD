using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using OnlineShopRestServer.Models;

namespace OnlineShopRestServer.Repositories
{
    public class TypeRepository
    {
        ShopContext db;

        public TypeRepository(ShopContext context)
        {
            this.db = context;
        }

        public IEnumerable<type> SelectAll()
        {
            return db.type;
        }

        public type Select(int id)
        {
            return (type)db.type.Where(type => type.id == id);
        }
        public IEnumerable<type> SelectByFilter(List<KeyValuePair<string, StringValues>> filter)
        {
            return FilterSelect<type>.GetWhere(filter, db);
        }

        public type Insert(type item)
        {
            db.type.Add(item);
            Save();
            return item;
        }

        public type Update(type item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }

        public int Delete(int id)
        {
            type item = db.type.Find(id);
            if (item != null)
                db.type.Remove(item);
            Save();
            return id;
        }
        private void Save()
        {
            db.SaveChanges();
        }
    }
}
