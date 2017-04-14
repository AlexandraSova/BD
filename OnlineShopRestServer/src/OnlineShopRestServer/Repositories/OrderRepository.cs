using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using OnlineShopRestServer.Models;

namespace OnlineShopRestServer.Repositories
{
    public class OrderRepository
    {
        ShopContext db;

        public OrderRepository(ShopContext context)
        {
            this.db = context;
        }

        public IEnumerable<order> SelectAll()
        {
            return db.order;
        }

        public order Select(int id)
        {
            return (order)db.order.Where(order => order.id == id);
        }
        public IEnumerable<order> SelectByFilter(List<KeyValuePair<string, StringValues>> filter)
        {
            return FilterSelect<order>.GetWhere(filter, db);
        }

        public order Insert(order item)
        {
            db.order.Add(item);
            Save();
            return item;
        }

        public order Update(order item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }

        public int Delete(int id)
        {
            order item = db.order.Find(id);
            if (item != null)
                db.order.Remove(item);
            Save();
            return id;
        }
        private void Save()
        {
            db.SaveChanges();
        }
    }
}
