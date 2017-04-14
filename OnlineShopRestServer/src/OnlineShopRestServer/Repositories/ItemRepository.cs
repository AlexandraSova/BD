using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using OnlineShopRestServer.Models;

namespace OnlineShopRestServer.Repositories
{
    public class ItemRepository
    {
        ShopContext db;

        public ItemRepository(ShopContext context)
        {
            this.db = context;
        }

        public IEnumerable<item> SelectAll()
        {
            return db.item;
        }

        public item Select(int id)
        {
            return (item)db.item.Where(item => item.id == id);
        }
        public IEnumerable<item> SelectByFilter(List<KeyValuePair<string, StringValues>> filter)
        {
            return FilterSelect<item>.GetWhere(filter, db);
        }

        public item Insert(item item)
        {
            db.item.Add(item);
            Save();
            return item;
        }

        public item Update(item item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }

        public int Delete(int id)
        {
            item item = db.item.Find(id);
            if (item != null)
                db.item.Remove(item);
            Save();
            return id;
        }
        private void Save()
        {
            db.SaveChanges();
        }
    }
}
