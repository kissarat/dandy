using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Text;
using System.Data.Entity.Validation;

namespace Johnny
{
    public abstract class ServerObject
    {
        private static TextWriter log;

        public static void OpenLog(string path = null)
        {
            path = path ?? HttpContext.Current.Server.MapPath(@"~/App_Data/Site.log");
            log = new StreamWriter(path, true, Encoding.UTF8);
        }

        [Conditional("TRACE")]
        public void Log(Exception ex, TraceEventType severity = TraceEventType.Error)
        {
            LogException(ex, severity);
        }

        [Conditional("TRACE")]
        public void Log(string message, TraceEventType severity = TraceEventType.Warning)
        {
            Log(this.GetType().Name, message, severity);
        }

        [Conditional("TRACE")]
        public static void Log(object sender, string message, TraceEventType severity = TraceEventType.Information)
        {

            Log(sender.GetType().Name, message, severity);
        }

        [Conditional("TRACE")]
        public static void Log(string sender, string message, TraceEventType severity = TraceEventType.Information)
        {

            log.WriteLine("{0};{1};{2}",
                severity.ToString(), sender, message);
        }

        [Conditional("TRACE")]
        public static void LogException(Exception ex, TraceEventType severity = TraceEventType.Error)
        {
            Log(ex.Source, ex.Message, severity);
        }

        //public static void LogException(DbEntityValidationException ex, TraceEventType severity = TraceEventType.Error)
        //{
        //    StringBuilder invalidEntries = new StringBuilder();
        //    foreach (var error in ex.EntityValidationErrors)
        //    {
        //        foreach (var valError in error.ValidationErrors)
        //        {
        //            invalidEntries.AppendFormat("{0}:{1},", valError.PropertyName, valError.ErrorMessage);
        //        }
        //    }
        //}
    }
}