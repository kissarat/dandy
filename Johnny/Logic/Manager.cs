using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johnny.Models;
using System.Timers;

namespace Johnny.Logic
{
    public class Manager : IDisposable
    {
        public static readonly string ParamName = "manager";
        public User User { get; set; }
        public bool IsStatic { get; set; }
        public static Manager Unregistered { get; private set; }

        static Manager()
        {
            Unregistered = new Manager
            {
                User = new User
                {
                    Id = -1,
                    IsVisible = false,
                    IsAdmin = true
                }
            };
        }

        //private Timer timer;
        public static Manager Current
        {
            get { return (Manager)HttpContext.Current.Session[ParamName]; }
            set { HttpContext.Current.Session[ParamName] = value; }
        }

        public Manager()
        {
            IsStatic = false;
            //timer = new Timer();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj is Manager)
            {
                return (obj as Manager).User.Id == User.Id;
            }
            return false;
        }
    }
}