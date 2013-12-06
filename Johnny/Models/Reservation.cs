using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Johnny.Models
{
    public class Reservation : Model
    {
        [Required]
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public int PeopleAmount { get; set; }
        public bool IsConfirmed { get; set; }
        public IEnumerable<Dish> Menu { get; set; }

        //public IEnumerable<Table> Tables
        //{
        //    get
        //    {
        //        int tablesAmount = Table.All.Count();
        //        int mask = 1;
        //        List<Table> tables = new List<Table>(4);
        //        foreach (var table in Table.All)
        //        {
        //            if (0 != (mask & TableMask))
        //            {
        //                tables.Add(table);
        //            }
        //        }
        //        return tables;
        //    }
        //}
    }
}