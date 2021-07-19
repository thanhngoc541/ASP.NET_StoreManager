using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext;

        public BaseRepository(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public BindingList<TEntity> GetAll()
        {
            DbContext.Set<TEntity>().Load();
            return DbContext.Set<TEntity>().Local.ToBindingList();
        }

        public TEntity Get(int primaryKey)
        {
            return DbContext.Set<TEntity>().Find(primaryKey);
        }

        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
             DbContext.SaveChanges();
        }
        

        public void Remove(int primaryKey)
        {
            var deleteMember = Get(primaryKey);
            if (deleteMember != null)
            {
                DbContext.Set<TEntity>().Remove(deleteMember);
                DbContext.SaveChanges();
            }
        }

    }
}
