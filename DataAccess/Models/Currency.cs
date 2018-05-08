using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccess.Models
{
    public class Currency
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insert name")]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Insert Symbol, only three character")]
        public string Symbol { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public double Tecaj { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy}")]
        public DateTime Ctime { get; set; }


        public virtual ICollection<Article> Articles { get; set; }
    }
}