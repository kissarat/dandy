using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Johnny.Models
{
    public class Table : Model
    {
        public static IEnumerable<Table> All;
        public int Capacity { get; set; }
    }
}