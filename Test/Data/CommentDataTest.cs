using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Johnny.Models;
using Johnny.Data;

namespace Johnny.Tests.Data
{
    [TestClass]
    public class CommentDataTest : RepositoryTest<Comment<Article>, CommentData<Article>>
    {
        private static User Author;
        private static Article Article;

        public CommentDataTest()
            : base(new CommentData<Article>(TestHelper.Context, TestHelper.Context.ArticleComments))
        {
            Author = UserDataTest.GenUser();
            TestHelper.DataContext.User.Create(Author);
            Article = ArticleDataTest.GenArticle();
            TestHelper.DataContext.Article.Create(Article);
        }

        #region Helpers

        public override void Gen(Comment<Article> comment)
        {
            GenComment(comment);
        }

        public static void GenComment(Comment<Article> comment)
        {
            comment.AuthorId = Author.Id;
            comment.TargetId = Article.Id;
            comment.Text = ArticleDataTest.GenSingleParagraph(TestHelper.Rand.Next(2, 8));
        }

        #endregion Helpers
    }
}
