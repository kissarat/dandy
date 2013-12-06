using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Data;
using Johnny.Models;

namespace Johnny.Logic
{
    public abstract class DefaultLogic<T, TRepository> : ServerObject
        where T : Model
        where TRepository : Repository<T>
    {
        protected abstract TRepository Repository { get; }


        [Admin]
        public virtual T Create()
        {
            var entity = Repository.Create();
            entity.Id = -1;
            entity.IsVisible = true;
            return entity;
        }

        [Admin]
        public virtual void Create(T entity)
        {
            Repository.Create(entity);
        }

        public virtual T Read(int id)
        {
            return Repository.Read(id);
        }

        public virtual IQueryable<T> Read()
        {
            return Repository.Read();
        }

        [Admin]
        public virtual void Save(T entity)
        {
            if (entity.Id > 0)
            {
                Repository.Update(entity);
            }
            else
            {
                Repository.Create(entity);
            }
        }

        [Admin]
        public virtual void Delete(int id)
        {
            Repository.Delete(id);
        }

        public virtual void ChangeVisibility(int id, bool isVisible)
        {
            Repository.Read(id).IsVisible = isVisible;
            Repository.SaveChanges();
        }
    }
}