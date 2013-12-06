using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Johnny.Models
{
    public class Page
    {
        private int skip = 0;
        private int size = defaultSize;

        public const int defaultSize = 10;
        public static readonly string NumberParam = "skip";
        public static readonly string SizeParam = "take";

        public int Skip
        {
            get { return skip; }
            set { skip = value; }
        }

        public int Number
        {
            get { return skip + 1; }
            set { skip = value - 1; }
        }

        public IEnumerable<T> Take<T>(IEnumerable<T> input)
        {
            return input.Skip(skip).Take(size);
        }

        //public static Page FromRequest()
        //{
        //    Page page = new Page();
        //    page.Skip = Helper.ReadParam(NumberParam);
        //    page.size = Helper.ReadParam(SizeParam);
        //}
    }
}