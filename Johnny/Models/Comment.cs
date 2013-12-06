using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Johnny.Models
{
    public class Comment<T> : Model
    {
        [ForeignKey("Target")]
        public int TargetId { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public string Text { get; set; }

        public T Target { get; set; }
        public User Author { get; set; }
    }
}