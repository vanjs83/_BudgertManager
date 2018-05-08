using _BudgetManager.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;

namespace BusinessLayer
{

    public class BusinessLayer
    {


        public static Article GetSmalliest(string s)
        {
            var data = DataLayer.GetView();
            Article minArticle = null;
            if (!string.IsNullOrEmpty(s) && data.Count(p => p.Currency.Symbol.Contains(s.ToUpper())) > 0 ) 
            {
                var max = data.Where(p => p.Currency.Symbol.Contains(s.ToUpper())).Min(k => k.Suma);
                minArticle = data.FirstOrDefault(p => p.Suma == max);
            }
            else
            {
                var max = data.Min(k => k.Suma);
                minArticle = data.FirstOrDefault(p => p.Suma == max);
            }
            return minArticle;
        }


        public static Article GetMax(string s)
        {
            var data = DataLayer.GetView();
            Article maxArticle = null;
            if (!string.IsNullOrEmpty(s) && data.Count(p => p.Currency.Symbol.Contains(s.ToUpper())) > 0)
            {
                var max = data.Where(p => p.Currency.Symbol.Contains(s.ToUpper())).Max(k => k.Suma);
                maxArticle = data.FirstOrDefault(p => p.Suma == max);
            }
            else
            { 
            var max = data.Max(k => k.Suma);
            maxArticle = data.FirstOrDefault(p => p.Suma == max);
             }
            return maxArticle;
        }


        public static List<Article> SortAsc()
        {
            var data = DataLayer.GetView();
            var sortdata = data.OrderBy(p => p.Suma).ToList();
            return sortdata;
        }


        public static decimal Convert(int? id)
        {
            var data = DataLayer.GetArticleId(id);
            var c = data.Suma * (decimal)data.Currency.Tecaj;
            return c;
        }



    }



}

