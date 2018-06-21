using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Portfolio.Web.Models;
using System.Data.Entity;
using Unity.Lifetime;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Unity.Injection;
using Portfolio.Web.Controllers;

namespace Portfolio.Web
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
		    container.RegisterType<DbContext, ApplicationDbContext>(
		        new HierarchicalLifetimeManager());
		    container.RegisterType<UserManager<ApplicationUser>>(
		        new HierarchicalLifetimeManager());
		    container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
		        new HierarchicalLifetimeManager());
		    container.RegisterType<AccountController>(
		        new InjectionConstructor());

            container.RegisterType<IPortfolioRepository, AppDbRepository>();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}