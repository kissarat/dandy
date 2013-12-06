using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using Johnny;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace Johnny.Data
{
    public class Repository<T> : ServerObject where T : Model
    {
        protected readonly Models.EntityContext context;
        protected readonly DbSet<T> dbSet;
        public DbSet<T> Entities
        {
            get { return dbSet; }
        }

        public virtual Repository<T> Raw
        {
            get
            {
                return this;
            }
        }

        public Repository(EntityContext context, DbSet<T> dbSet)
        {
            this.context = context;
            this.dbSet = dbSet;
        }

        public virtual T Create()
        {
            return dbSet.Create();
        }

        /// <summary>
        /// Створює нову сутність
        /// </summary>
        /// <param name="entity">Сутність</param>
        public virtual void Create(T entity)
        {
            dbSet.Add(entity);
            SaveChanges();
        }

        public virtual T CreateReturn(T entity)
        {
            Create(entity);
            return dbSet.Single(e => e.Id == entity.Id);
        }

        public virtual T Read(int id)
        {
            return dbSet.Single(e => e.Id == id);
        }

        public virtual IQueryable<T> Read()
        {
            return dbSet;
        }

        public virtual void Update(T entity)
        {
            context.Entry(Read(entity.Id)).CurrentValues.SetValues(entity);
            //context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public virtual void Delete(int id)
        {
            dbSet.Remove(Read(id));
            SaveChanges();
        }

        public virtual T TryRead(int id)
        {
            return dbSet.SingleOrDefault(e => e.Id == id);
        }

        public virtual void SaveChanges()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            
        }
    }
}