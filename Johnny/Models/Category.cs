using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Johnny.Models
{
    public class Category : Model
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
    }
}