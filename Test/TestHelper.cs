using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Johnny.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Objects;
using System.Data.Entity;
using Johnny.Data;
using Johnny.Tests.Data;
using System.Diagnostics;

namespace Johnny.Tests
{
    public static class TestHelper
    {
        internal static readonly Random Rand;
        internal static readonly EntityContext Context;
        internal static readonly DataContext DataContext;
        internal static readonly Database Database;
        internal static bool IsHumanNames { get; set; }

        static TestHelper()
        {
            ServerObject.OpenLog("Site.log");
            Rand = new Random();
            Context = new EntityContext();
            DataContext = new DataContext(Context);
            Database = Context.Database;
            IsHumanNames = true;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityContext>());
            Database.Initialize(false);
            ServerObject.Log("TestHelper", "Initilized", TraceEventType.Start);

        }

        [AssemblyInitialize]
        public static void Initialize()
        {
            
        }

        public static void Main()
        {
            Initialize();
            int amount = Rand.Next(20, 50);
            int articlesAmount;
            User user;
            for (int i = 0; i < amount; i++)
            {
                user = UserDataTest.GenUser();
                Context.Users.Add(user);
                Context.SaveChanges();
                Debug.Write(string.Format("Gen {0}:{1} with ", user.Id, user.Login));
                if (user.IsAdmin)
                {
                    articlesAmount = Rand.Next(3);
                    Debug.WriteLine("{0} articles ", articlesAmount);
                    for (int j = 0; j < articlesAmount; j++)
                    {
                        Context.Articles.Add(ArticleDataTest.Gen(user.Id));
                    }
                }
            }
            Category category;
            int number;
            foreach (string categoryName in TestData.Categories)
            {
                category = new Category {Name = categoryName, IsVisible = true};
                Context.DishCategories.Add(category);
                Context.SaveChanges();
                number = Rand.Next(4, 12);
                for (int i = 0; i < number; i++)
                {
                    Context.Menu.Add(MenuDataTest.GenDish(category.Id));
                }
            }
            Context.SaveChanges();
            var queue = new Queue<Dish>();
            foreach (var dish in Context.Menu)
            {
                number = Rand.Next(queue.Count);
                for (int i = 0; i < number; i++)
                {
                    dish.Delicious.Add(queue.Dequeue());
                }
                queue.Enqueue(dish);
            }
            Context.SaveChanges();
        }

        internal static char GenChar(char min, char max)
        {
            return (char)Rand.Next((int)min, (int)max + 1);
        }

        internal static string GetRand(string[] array)
        {
            return array[Rand.Next(array.Length)];
        }

        internal static string GenLogin(bool isMale)
        {
            return (    isMale
                ?       GetRand(TestData.MaleNames)
                :       GetRand(TestData.FemaleNames)
                ) +     GetRand(TestData.Surnames);
        }

        internal static string GenString(Func<char> generator, int max = 20, int min = 5)
        {
            int length = 0;
            if (max > min)
            {
                length = Rand.Next(min, max);
            }
            else if (max == min)
            {
                length = max;
            }
            else
            {
                throw new ArgumentException();
            }

            char[] generated = new char[length];
            for (int i = 0; i < length; i++)
            {
                generated[i] = generator();
            }
            return new string(generated);
        }

        internal static char GenPasswordChar()
        {
            return GenChar('!', '~');
        }

        internal static string GetAny(this string [] array)
        {
            return array[Rand.Next(array.Length)];
        }

        internal static char GenLiteral()
        {
            double rand = Rand.NextDouble();
            if (0 <= rand && rand < 0.2)
            {
                return GenChar('A', 'Z');
            }
            else if (0.2 <= rand && rand < 0.98)
            {
                return GenChar('a', 'z');
            }
            else
            {
                return '_';
            }
        }

        internal static bool GenBoolean(double threshold = 0.5)
        {
            return Rand.NextDouble() > threshold;
        }

        internal static T GenNullable<T>(Func<T> generator) where T : class
        {
            return GenBoolean() ? generator() : null;
        }

        public static T GetAny<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.ElementAtOrDefault(Rand.Next(enumerable.Count()));
        }
    }
}
