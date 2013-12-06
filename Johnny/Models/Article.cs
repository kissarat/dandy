using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Johnny.Models
{
    public class Article : Model
    {
        #region Properties

        [ForeignKey("Author")]
        [HiddenInput(DisplayValue=false)]
        public virtual int AuthorId { get; set; }

        [Required]
        //[MaxLength(130)]
        [Display(Name = "Заголовок")]
        [DataType(DataType.Text)]
        public virtual string Title { get; set; }

        //[StringLength(1000, MinimumLength = 50)]
        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }

        [Required]
        //[MinLength(200)]
        [Display(Name = "Текст")]
        [DataType(DataType.MultilineText)]
        public virtual string Text { get; set; }

        [Display(Name = "Дата публікації")]
        [DataType(DataType.Date)]
        public virtual DateTime PublicationDate { get; set; }

        #endregion Properties
        #region Associated entities

        public virtual User Author { get; set; }

        #endregion Associated entities
    }
}