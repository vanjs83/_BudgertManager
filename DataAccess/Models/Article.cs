using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.Models
{
    public class Article
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insert name")]
        public string Name { get; set; }
        [Required]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [RegularExpression(@"^\d{0,9}$", ErrorMessage = "Price can't have more than 2 decimal places")]
        public decimal Suma { get; set; }

        [Required]
        public DateTime Ctime { get; set; }
       // 9.4.2018. 0:00:00


        [ForeignKey("Pay")]
        public int? PayID { get; set; }
        public Pay Pay { get; set; }

        [ForeignKey("Category")]
        public int? CategoryID { get; set; }
        public Category Category { get; set; }


        [ForeignKey("Currency")]
        public int? CurrencyID { get; set; }
        public Currency Currency { get; set; }
    }
}