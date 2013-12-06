using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Johnny.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Johnny.Data;

namespace Johnny.Tests.Data
{
    [TestClass]
    public class UserDataTest : RepositoryTest<User, Repository<User>>
    {
        public UserDataTest()
            :base(new Repository<User>(TestHelper.Context, TestHelper.Context.Users))
        {
        }

        #region Helpers

        //private static int userCount = 0;

        private static List<int> UserIDs = new List<int>();

        public int GetAnyID()
        {
            return UserIDs.ElementAt(TestHelper.Rand.Next(UserIDs.Count));
        }

        public override User Gen()
        {
            return GenUser();
        }

        public override void Gen(User user)
        {
            GenUser(user);
        }

        internal static User GenUser()
        {
            var user = TestHelper.Context.Users.Create();
            GenUser(user);
            return user;
        }

        internal static void GenUser(User user)
        {
            //userCount++;
            user.IsMale = TestHelper.GenBoolean();
            user.Login = TestHelper.IsHumanNames
                ? TestHelper.GenLogin(user.IsMale)
                : TestHelper.GenString(TestHelper.GenLiteral, 31, 2);
            user.SetPassword(TestHelper.GenString(
                TestHelper.GenPasswordChar, 21, 4));
            user.Email = GenMail(user.Login);
            user.Phone = TestHelper.GenNullable(GenPhone);
            user.IsEmailVerified = TestHelper.GenBoolean();
            user.IsPhoneVisible = TestHelper.GenBoolean();
            user.IsEmailVisible = TestHelper.GenBoolean();
            user.IsVisible = TestHelper.GenBoolean();
            user.IsOnline = false;
            user.IsLockedOut = false;
            user.FailedPasswordAttemptCount = 0;
            user.IsAdmin = TestHelper.GenBoolean();
        }

        //internal static UserRole GenRole()
        //{
        //    switch (TestHelper.Rand.Next(3))
        //    {
        //        case 0:
        //            return UserRole.Client;
        //        case 1:
        //            return UserRole.TrustedClient;
        //        case 2:
        //            return UserRole.Undefined;
        //        default:
        //            return UserRole.Undefined;
        //    }
        //}

        internal static string GenMail(string login = null)
        {
            return (TestHelper.IsHumanNames
                ? login : TestHelper.GenString(TestHelper.GenLiteral, 40, 2)
                ) + "@test9.com";
        }

        internal static string GenPhone()
        {
            return "380" + TestHelper.Rand.Next(100000000, 999999999);
        }

        #endregion Helpers
    }
}
