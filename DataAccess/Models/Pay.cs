using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccess.Models
{
    public class Pay
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insert name")]
        public string Name { get; set; }

      
        public DateTime Ctime { get; set; }
   

        public virtual ICollection<Article> Articles { get; set; }


    }
}