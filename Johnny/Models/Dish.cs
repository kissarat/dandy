using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Johnny.Data;

namespace Johnny.Models
{
    public class Dish : Model
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(80, MinimumLength = 1)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public short Amount { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Dish> Delicious { get; set; }

        public Dish()
        {
            Delicious = new HashSet<Dish>();
        }

        public static void Configurate(EntityTypeConfiguration<Dish> config)
        {
            config.HasMany(e => e.Delicious).WithMany()
            .Map(m => m.ToTable("Delicious"));
        }
    }
}