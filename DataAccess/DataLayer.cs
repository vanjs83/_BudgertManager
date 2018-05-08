using _BudgetManager.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;

namespace DataAccess
{
    public class DataLayer
    {

        public static void CreateModel(Article model)

        {
            var context = new BudgetManagerDbContext();
            context.Items.Add(model);
            context.SaveChanges();
            context.Dispose();

        }

        public static List<Article> GetView()
        {
            var context = new BudgetManagerDbContext();

            var data = context.Items.Include(p => p.Category)
                                    .Include(p => p.Currency)
                                    .Include(P => P.Pay)
                                    .ToList();
            //Oslobađanje memorije 
            context.Dispose();
            return data;
        }

        public static List<Article> GetArticleFilter(ArticleFilterModel filter)
        {
            var context = new BudgetManagerDbContext();

            var dataQuery = context.Items
                .Include(p => p.Category)
                .Include(p => p.Currency)
                .Include(p => p.Pay)
                .ToList()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.ArticleName))
                dataQuery =dataQuery.Where(p => p.Name.Contains(filter.ArticleName));

            if (!string.IsNullOrWhiteSpace(filter.CategoryName))
                dataQuery = dataQuery.Where(p => p.Category != null && p.Category.Name.Contains(filter.CategoryName));

            if (!string.IsNullOrWhiteSpace(filter.CurrencySymbol))
                dataQuery = dataQuery.Where(p => p.Currency != null && p.Currency.Symbol.Contains(filter.CurrencySymbol));

            if (!string.IsNullOrWhiteSpace(filter.PayName))
                dataQuery = dataQuery.Where(p => p.Pay != null && p.Pay.Name.Contains(filter.PayName));

            //Oslobađanje memorije 
            context.Dispose();
            return dataQuery.ToList();

        }


        public static Article GetArticleId(int? id)
        {
            var context = new BudgetManagerDbContext();

            var data = context.Items.Include(p => p.Category)
                                    .Include(p => p.Currency)
                                    .Include(P => P.Pay)
                                    .Where(p => p.Id == id).FirstOrDefault();

            //Oslobađanje memorije 
            context.Dispose();
            return data;
        }


        public static bool EditArticle(int? id, Article model)
        {

            var context = new BudgetManagerDbContext();
            var Olddata = context.Items
                                .Include(p => p.Category)
                                .Include(p => p.Currency)
                                .Include(P => P.Pay)
                                .Where(p => p.Id == id)
                                .FirstOrDefault();
           
            context.Entry(Olddata).CurrentValues.SetValues(model);
            context.SaveChanges();
            context.Dispose();
            return true;

        }


        public static bool DeleteArticle(int? id)
        {

            var context = new BudgetManagerDbContext();


            var delModel = context.Items
                 .Where(x => x.Id == id)
                  .FirstOrDefault();

            if (delModel != null)
            {
                context.Entry(delModel).State = System.Data.Entity.EntityState.Deleted;
                context.Items.Remove(delModel);
                context.SaveChanges();
                context.Dispose();
                return true;
            }
            return false;
        }


        public static List<Currency> GetListCirrency()
        {
            //Kreiranje konteksta 
            var context = new BudgetManagerDbContext();

            var Currencys = context.Currencies.ToList();
            context.Dispose();
            return Currencys;
        }

        public static List<Category> GetListCategory()
        {
            //Kreiranje konteksta 
            var context = new BudgetManagerDbContext();

            var Categorys = context.Categories.ToList();
            context.Dispose();
            return Categorys;
        }

        public static List<Pay> GetListTypeOfPay()
        {
            //Kreiranje konteksta 
            var context = new BudgetManagerDbContext();

            var TypeOfPay = context.Pays.ToList();
            context.Dispose();
            return TypeOfPay;
        }


        public static List<Category> GetViewCategory()
        {

            var context = new BudgetManagerDbContext();

            var data = context.Categories
                              .Include(p => p.Articles)
                              .ToList();

            //Oslobađanje memorije 
            context.Dispose();
            return data;
        }

        public static void CreateCategory(Category model)
        {
            var context = new BudgetManagerDbContext();
            context.Categories.Add(model);
            context.SaveChanges();
            context.Dispose();

        }



        public static Category GetCategoryId(int? id)
        {

            var context = new BudgetManagerDbContext();
            var model = context.Categories
                        .Where(p => p.Id == id)
                        .FirstOrDefault();
            context.Dispose();
            return model;
        }



        public static bool EditCategory(int? id, Category model)
        {

            var context = new BudgetManagerDbContext();
          
            var OldModel = context.Categories.Where(p => p.Id == id).FirstOrDefault();

            context.Entry(OldModel).CurrentValues.SetValues(model);
            context.SaveChanges();
            context.Dispose();
            return true;
        }

        public static bool DeleteCategory(int? id)
        {
            var context = new BudgetManagerDbContext();
            //            MockCompanyRepository.Instance.Delete(id);

            var delModel = context.Categories
                 .Where(x => x.Id == id)
                  .FirstOrDefault();

            //Postavljanje stanja na deleted - označavanje da je Company obrisan 
            if (delModel != null)
            {
                context.Entry(delModel).State = System.Data.Entity.EntityState.Deleted;
                context.Categories.Remove(delModel);
                context.SaveChanges();
                context.Dispose();

                return true;

            }
            return false;
        }
    }
}
