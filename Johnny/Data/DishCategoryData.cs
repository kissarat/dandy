using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Johnny.Models;

namespace Johnny.Data
{
    public class DishCategoryData : Repository<Category>
    {
        public static IEnumerable<Category> Categories { get; private set; }

        public DishCategoryData(EntityContext context, DbSet<Category> dbSet) : base(context, dbSet)
        {
            if (null == Categories)
            {
                Categories = Read().ToList();
            }
        }
    }
}