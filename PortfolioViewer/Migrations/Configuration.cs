namespace PortfolioViewer.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PortfolioViewer.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PortfolioViewer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "PortfolioViewer.Models.ApplicationDbContext";
        }

        protected override void Seed(PortfolioViewer.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Users.Any(u => u.Email == "bjorn@devnull.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "bjorn@devnull.com", UserName = "bjorn@devnull.com", FirstName = "Bear", LastName = "Little",CustomerID = 12};

                manager.Create(user, "Lidell1!");
            }
        }
    }
}
