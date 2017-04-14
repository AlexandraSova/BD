using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using OnlineShopRestServer.Models;

namespace OnlineShopRestServer.Repositories
{
    public class ProductRepository
    {
        ShopContext db;

        public ProductRepository(ShopContext context)
        {
            this.db = context;
        }

        public IEnumerable<product> SelectAll()
        {
            return db.product;
        }

        public product Select(int id)
        {
            return (product)db.product.Where(product => product.id == id);
        }
        public IEnumerable<product> SelectByFilter(List<KeyValuePair<string, StringValues>> filter)
        {
            return FilterSelect<product>.GetWhere(filter, db);
        }

        public product Insert(product item)
        {
            db.product.Add(item);
            Save();
            return item;
        }

        public product Update(product item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }

        public int Delete(int id)
        {
            product item = db.product.Find(id);
            if (item != null)
                db.product.Remove(item);
            Save();
            return id;
        }
        private void Save()
        {
            db.SaveChanges();
        }
    }
}
