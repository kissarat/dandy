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
    public class ArticleDataTest : RepositoryTest<Article, ArticleData>
    {
        private static User Author;

        public ArticleDataTest()
            :base(new ArticleData(TestHelper.Context, TestHelper.Context.Articles))
        {}

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            Author = UserDataTest.GenUser();
            Author.IsAdmin = true;
            TestHelper.DataContext.User.Create(Author);
        }

        //[TestMethod]
        public void AuthorExists()
        {
            Assert.IsNotNull(TestHelper.DataContext.User.TryRead(Author.Id), Author.Id.ToString());
        }

        #region Helpers

        public override void Gen(Article article)
        {
            GenArticle(article);
        }

        internal static Article Gen(int authorId)
        {
            Article article = TestHelper.Context.Articles.Create();
            Gen(article, authorId);
            return article;
        }

        internal static Article GenArticle()
        {
            var article = new Article();
            GenArticle(article);
            return article;
        }
        
        public static void GenArticle(Article article)
        {
            Gen(article, Author.Id);
        }

        public static void Gen(Article article, int authorId)
        {
            article.AuthorId = authorId;
            article.Title = GenTitle();
            article.Description = GenSingleParagraph();
            article.Text = GenText();
            article.PublicationDate = DateTime.Now;
        }

        public static string GenTitle()
        {
            return TestData.Titles[TestHelper.Rand.Next(TestData.Titles.Length)];
        }

        public static string GenText(int sentencesNumber = 15)
        {
            int paragraphSize;
            var builder = new StringBuilder();
            for (int i = 0; i < sentencesNumber;)
            {
                paragraphSize = TestHelper.Rand.Next(3, 7);
                if (sentencesNumber < (i + paragraphSize))
                {
                    paragraphSize = sentencesNumber - paragraphSize;
                }
                GenParagraph(builder, paragraphSize);
                builder.AppendLine();
                i += paragraphSize;
            }
            return builder.ToString();
        }

        public static string GenSingleParagraph(int sentencesNumber = 3)
        {
            var builder = new StringBuilder();
            GenParagraph(builder, sentencesNumber);
            return builder.ToString();
        }

        public static void GenParagraph(StringBuilder builder, int sentencesNumber = 10)
        {
            for (int i = 0; i < sentencesNumber; i++)
            {
                builder.Append(TestData.Sentences[TestHelper.Rand.Next(TestData.Sentences.Length)]);
                builder.Append(". ");
            }
        }

        #endregion Helpers
    }
}
