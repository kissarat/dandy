using Johnny.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Johnny.Models;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Data.Entity.Validation;
using System.Text;

namespace Johnny.Tests.Data
{
    [TestClass]
    public abstract class RepositoryTest<T, TRepository>
        where T : Model, new()
        where TRepository : Repository<T>
    {
        private TRepository repository;
        protected Repository<T> Raw;
        protected TRepository Repository
        {
            get { return repository; }
            private set
            {
                repository = value;
                Raw = value.Raw;
            }
        }

        private TestContext testContext;

        public TestContext TestContext
        {
            get
            {
                return testContext;
            }
            set
            {
                testContext = value;
            }
        }

        #region Additional test attributes

        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //    //TestHelper.Context.Configuration.ValidateOnSaveEnabled = false;
        //}
        
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        public RepositoryTest(TRepository repository)
        {
            Repository = repository;
        }

        [TestMethod]
        public void CreateReturnTest()
        {
            try
            {
                T expected = Gen();
                T actual = Raw.CreateReturn(expected);
                Assert.AreEqual(actual, expected);
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var val in ex.EntityValidationErrors)
                {
                    foreach (var error in val.ValidationErrors)
                    {
                        builder.Append(error.PropertyName);
                        builder.Append(": ");
                        builder.Append(error.ErrorMessage);
                        builder.Append(", ");
                    }
                }
                Assert.Fail(builder.ToString());
            }
            
        }

        [TestMethod]
        public void UpdateTest()
        {
            T expected = Gen();
            T actual;
            expected = Raw.CreateReturn(expected);
            Gen(expected);
            Raw.Update(expected);
            actual = Raw.Read(expected.Id);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void DeleteTest()
        {
            T user = Raw.CreateReturn(Gen());
            Raw.Delete(user.Id);
            Assert.IsNull(Raw.TryRead(user.Id));
        }

        public virtual T Gen()
        {
            T entity = Raw.Entities.Create();
            Gen(entity);
            return entity;
        }

        public abstract void Gen(T entity);
    }
}
