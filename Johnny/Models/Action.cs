using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Johnny.Models
{
    public class Action : Article
    {
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}