using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {

        internal DataContext context;
        internal DbSet<T> dBSet;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dBSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dBSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if(context.Entry(t).State == EntityState.Detached)
            {
                dBSet.Attach(t);
            }

            dBSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dBSet.Find(Id);
        }

        public void Insert(T t)
        {
          dBSet.Add(t);
        }

        public void Update(T t)
        {
            dBSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
