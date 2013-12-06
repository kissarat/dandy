using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using System.Data.Entity;

namespace Johnny.Data
{
    public partial class UserData : Repository<User>
    {
        public UserData(EntityContext context, DbSet<User> dbSet)
            : base(context, dbSet)
        {
        }

        public IQueryable<User> ReadAdmins()
        {
            return context.Users.Where(u => u.IsAdmin);
        }
    }
}