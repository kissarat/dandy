using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny;
using Johnny.Models;
using System.Diagnostics;

namespace Johnny.Data
{
    public class DataContext : IDisposable
    {
        public static readonly string ParamName = "data";
        public readonly string ErrorContextDisposed = "Database context disposed";
        private EntityContext context;
        private static readonly DataContext current;
        private UserData user;
        private ArticleData article;
        private MenuData menu;
        //private CommentData<Article> articleComments;

        public static DataContext Current
        {
            get { return (DataContext)HttpContext.Current.Session[ParamName]; }
            set { HttpContext.Current.Session[ParamName] = value; }
        }

        public EntityContext Context
        {
            get
            {
                return context;
            }
        }

        public DataContext()
        {
            context = new EntityContext();
        }

        public DataContext(EntityContext context)
        {
            this.context = context;
        }

        public UserData User
        {
            get
            {
                if (null == user)
                {
                    checkContext();
                    user = new UserData(context, context.Users);
                }
                return user;
            }
        }

        public ArticleData Article
        {
            get
            {
                if (null == article)
                {
                    checkContext();
                    article = new ArticleData(context, context.Articles);
                }
                return article;
            }
        }

        public MenuData Menu
        {
            get
            {
                if (null == menu)
                {
                    checkContext();
                    menu = new MenuData(context, context.Menu);
                }
                return menu;
            }
        }

        //public CommentData<Article> ArticleComments
        //{
        //    get
        //    {
        //        if (null == articleComments)
        //        {
        //            checkContext();
        //            articleComments = new CommentData<Article>(context, context.ArticleComments);
        //        }
        //        return articleComments;
        //    }
        //}

        [Conditional("DEBUG")]
        private void checkContext()
        {
            if (null == context)
            {
                throw new ObjectDisposedException(ErrorContextDisposed);
            }
        }

        public void Dispose()
        {
            context.Dispose();
            context = null;
            article = null;
        }

        public static void DisposeContext()
        {
            Current.Dispose();
            Current = null;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}