using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using System.Data.Entity;

namespace Johnny.Data
{
    public class CommentData<T> : Repository<Comment<T>>
    {
        public CommentData(EntityContext context, DbSet<Comment<T>> dbSet)
            : base(context, dbSet)
        { }
    }
}