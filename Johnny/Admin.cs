using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using Johnny.Logic;

namespace Johnny
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class Admin : Attribute
    {
        public bool IsAllow { get; set; }

        public Admin()
        {
            IsAllow = Manager.Current.User.IsAdmin;
        }
    }
}