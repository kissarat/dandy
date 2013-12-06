using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Johnny.Models
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Видимість")]
        public bool IsVisible { get; set; }

        //[Timestamp]
        //public DateTime Modified { get; set; }
    }
}