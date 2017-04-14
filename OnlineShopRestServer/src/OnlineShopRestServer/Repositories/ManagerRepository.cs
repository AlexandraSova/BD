using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using OnlineShopRestServer.Models;

namespace OnlineShopRestServer.Repositories
{
    public class ManagerRepository
    {
        ShopContext db;

        public ManagerRepository(ShopContext context)
        {
            this.db = context;
        }

        public IEnumerable<manager> SelectAll()
        {
            return db.manager;
        }

        public manager Select(int id)
        {
            return (manager)db.manager.Where(manager => manager.id == id);
        }
        public IEnumerable<manager> SelectByFilter(List<KeyValuePair<string, StringValues>> filter)
        {
            return FilterSelect<manager>.GetWhere(filter, db);
        }

        public manager Insert(manager item)
        {
            db.manager.Add(item);
            Save();
            return item;
        }

        public manager Update(manager item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }

        public int Delete(int id)
        {
            manager item = db.manager.Find(id);
            if (item != null)
                db.manager.Remove(item);
            Save();
            return id;
        }
        private void Save()
        {
            db.SaveChanges();
        }
    }
}
