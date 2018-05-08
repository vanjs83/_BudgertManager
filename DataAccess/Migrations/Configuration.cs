namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Models.BudgetManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.Models.BudgetManagerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Currencies.AddOrUpdate(
            h => h.Name,   // Use Name (or some other unique field) instead of Id
       new Models.Currency
       {
           Name = "Franak",
           Symbol = "CHF",
           Tecaj = 6.202,
           Ctime = DateTime.UtcNow
       },
       new Models.Currency
       {
           Name = "Funta",
           Symbol = "GBP",
           Tecaj = 8.462,
           Ctime = DateTime.UtcNow
       }
       
       );

            context.SaveChanges();


        }
    }
}
