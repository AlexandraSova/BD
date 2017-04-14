using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Primitives;
using OnlineShopRestServer.Models;

namespace OnlineShopRestServer
{
    public class FilterSelect<T> where T : class
    {
        private static string ElementTypeName
        {
            get
            {
                return typeof(T).Name;
            }
        }
        public static IEnumerable<T> GetWhere(List<KeyValuePair<string, StringValues>> criterias, ShopContext db)
        {
            var t = DynamicWhereExp(criterias);
            return db.Set<T>().Where(t).ToList();
        }
        public static Expression<Func<T, bool>> DynamicWhereExp(List<KeyValuePair<string, StringValues>> criterias)
        {
            ParameterExpression Param = Expression.Parameter(typeof(T), ElementTypeName);

            Expression exp = WhereExp(criterias[0].Key, criterias[0].Value, Param);
            for (int i=1;i<criterias.Count();i++)
            {
                exp = Expression.And(exp, WhereExp(criterias[i].Key, criterias[i].Value, Param));     
            }

            return Expression.Lambda<Func<T, bool>>(exp, new ParameterExpression[] { Param });
        }
        private static Expression WhereExp(string key, string value, ParameterExpression param)
        {
            PropertyInfo prop = typeof(T).GetProperty(key);
            Expression aLeft = Expression.Property(param, prop);
            Expression aRight;
            Type t = prop.PropertyType;
            if (t == typeof(Int32))
            {
                aRight = Expression.Constant(Convert.ToInt32(value));
            }
            else
            {
                aRight = Expression.Constant(value);
            }
            Expression typeCheck = Expression.Equal(aLeft, aRight);

    
            return typeCheck;
        }

        
    }
}
