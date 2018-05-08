using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Models
{
    public class ArticleFilterModel
    {
       public string ArticleName  { get; set; } 
       public string CategoryName { get; set; }
       public string CurrencySymbol { get; set; }
       public string PayName      { get; set; }
    }
}