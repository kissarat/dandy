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
    public class MenuDataTest : RepositoryTest<Dish, MenuData>
    {
        public MenuDataTest()
            :base(new MenuData(TestHelper.Context, TestHelper.Context.Menu))
        {}

        #region Helpers

        public override void Gen(Dish dish)
        {
            dish.CategoryId = 0;
            GenDish(dish);
        }

        public static Dish GenDish(int categoryId)
        {
            var dish = TestHelper.Context.Menu.Create();
            dish.CategoryId = categoryId;
            GenDish(dish);
            return dish;
        }

        public static void GenDish(Dish dish)
        {
            dish.Name = TestData.Surnames.GetAny();
            dish.PreparationTime = TimeSpan.FromMinutes(TestHelper.Rand.Next(5, 60));
            dish.Price = new decimal(TestHelper.Rand.Next(5, 300));
            dish.Amount = (short) TestHelper.Rand.Next(20, 500);
            dish.IsVisible = TestHelper.GenBoolean(0.15);
        }

        #endregion Helpers
    }
}
