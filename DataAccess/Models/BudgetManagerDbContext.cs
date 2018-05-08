
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using _BudgetManager;

namespace DataAccess.Models
{
    public class BudgetManagerDbContext : IdentityDbContext<User>
    {
        public BudgetManagerDbContext() : base() { }

        public static BudgetManagerDbContext Create()
        {
            return new BudgetManagerDbContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Items { get; set; }
        public DbSet<Pay> Pays { get; set; }
        public DbSet<Currency> Currencies { get; set; }


    }
}