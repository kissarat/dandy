using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using Johnny.Data;

namespace Johnny.Logic
{
    public class MenuLogic : DefaultLogic<Dish, MenuData>
    {
        private Repository<Category> categoryData;

        protected override MenuData Repository
        {
            get { return DataContext.Current.Menu; }
        }

        public MenuLogic()
        {
            var context = DataContext.Current.Context;
            categoryData = new DishCategoryData(context, context.DishCategories);
        }

        public override Dish Read(int id)
        {
            var dish = base.Read(id);
            dish.Delicious.RemoveInvisible();
            return dish;
        }

        public override IQueryable<Dish> Read()
        {
            return base.Read().OrderBy(d => d.Name);
        }

        public IQueryable<Category> ReadCategories()
        {
            return categoryData.Read();
        }
    }
}