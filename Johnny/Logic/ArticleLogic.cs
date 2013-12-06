using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using Johnny.Data;

namespace Johnny.Logic
{
    public class ArticleLogic : DefaultLogic<Article, ArticleData>
    {
        protected override ArticleData Repository
        {
            get { return DataContext.Current.Article; }
        }

        public override Article Create()
        {
            var article = base.Create();
            article.Author = Manager.Current.User;
            article.AuthorId = article.Author.Id;
            return article;
        }
    }
}