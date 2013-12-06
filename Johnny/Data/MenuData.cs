using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using System.Data.Entity;

namespace Johnny.Data
{
    public class MenuData : Repository<Dish>
    {
        public MenuData(EntityContext context, DbSet<Dish> dbSet)
            : base(context, dbSet)
        { }

        public override Dish Read(int id)
        {
            return Fixup(Read()).Single(e => e.Id == id);
        }

        public IQueryable<Dish> Fixup(IQueryable<Dish> entitySet)
        {
            return entitySet.Include(a => a.Delicious);
        }
    }
}