using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using System.Data.Entity;

namespace Johnny.Data
{
    public class ArticleData : Repository<Article>
    {
        public ArticleData(EntityContext context, DbSet<Article> dbSet)
            : base(context, dbSet)
        {
        }

        public override Repository<Article> Raw
        {
            get { return this as Repository<Article>; }
        }

        public IQueryable<Article> Fixup(IQueryable<Article> entitySet)
        {
            return entitySet.Include(a => a.Author);
        }
    }
}