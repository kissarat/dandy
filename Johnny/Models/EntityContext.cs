using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Reflection;
using Johnny.Data;
using System.Diagnostics;

namespace Johnny.Models
{
    public class EntityContext : DbContext, IDatabaseInitializer<EntityContext>
    {
        public virtual DbSet<Article>       Articles { get; set; }
        public virtual DbSet<Action>        Actions { get; set; }
        public virtual DbSet<Dish>          Menu { get; set; }
        public virtual DbSet<Category>      DishCategories { get; set; }
        public virtual DbSet<Reservation>   Reservation { get; set; }
        public virtual DbSet<User>          Users { get; set; }

        public EntityContext()
        {

        }

        public EntityContext(string connectionString)
            :base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder b)
        {
            var users       = b.Entity<User>().HasEntitySetName("Users");
            var articles    = b.Entity<Article>().HasEntitySetName("Articles");
            Dish.Configurate(b.Entity<Dish>());
            //var events      = b.Entity<Action>().HasEntitySetName("Actions");
            //var dishes      = b.Entity<Dish>().HasEntitySetName("Dishes");
            //var reservation = b.Entity<Dish>().HasEntitySetName("Reservation");
            //var notabene    = b.Entity<Article>().HasEntitySetName("NotaBene");

            //articles.HasRequired(a => a.Author);

            base.OnModelCreating(b);
        }       

        public void InitializeDatabase(EntityContext context)
        {
            try
            {
                if (Database.CreateIfNotExists())
                {
                    //Seed();
                    ServerObject.Log("InitializeDatabase", "Database created");
                }
            }
            catch (Exception ex)
            {
                ServerObject.LogException(ex);
            }
        }

        public virtual void Seed()
        {
            Configuration.ValidateOnSaveEnabled = false;
            var data = new DataContext(this);

            //var user = new User
            //{
            //    Login = "Admin",
            //    Password = "ad67neb1",
            //    Email = "johnny@gmail.com",
            //    IsEmailVerified = true,
            //    Role = UserRole.Admin
            //};
            //user = data.User.CreateReturn(user);

            var article = new Article
            {
                AuthorId = 1,
                Title = "Перша стаття",
                Description = "Опис",
                Text = "Вміс статті",
                PublicationDate = DateTime.Now,
                IsVisible = true
            };
            article = data.Article.CreateReturn(article);
        }

        //public virtual DbSet<IModel> FindSet<T>()
        //{
        //    var setProperties = typeof(EntityContext).GetProperties(BindingFlags.Public);
        //    foreach (var setProperty in setProperties)
        //    {
        //        if (setProperty.PropertyType.Equals())
        //        {
        //            return (virtual DbSet<IModel>) setProperty.GetValue(this, null);
        //        }
        //    }
        //    return null;
        //}
    }
}