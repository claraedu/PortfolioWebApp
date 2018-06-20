using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PortfolioViewer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PortfolioViewer.Controllers
{
	public class AdminAuthorize : AuthorizeAttribute
	{
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new HttpUnauthorizedResult();
			}
			else
			{
				filterContext.Result = new RedirectToRouteResult(new
					RouteValueDictionary(new { controller = "Admin/NoCanDoHombre" }));
			}
		}
	}

	[AdminAuthorize(Roles = "Administrator")]
	public class AdminController : Controller
    {

		public ApplicationUserManager UserManager
		{
			get
			{
				return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
		}

		// GET: Admin
		public ActionResult Index()
        {
			var user = UserManager.FindById(User.Identity.GetUserId());

			SQLController sqlController = new SQLController();
			IEnumerable<CustomDBUser> users =  sqlController.adminListUsersAndRoles(user.Id);

			return View(users);
        }

		public string NoCanDoHombre()
		{
			return "Im sorry, No can do, Hombre.";
		}



	}

	class SQLController
	{

		private string connectionString = "server=(local)\\SQLEXPRESS;database=Portfolio;integrated Security=SSPI;";

		public IEnumerable<CustomDBUser> adminListUsersAndRoles(string userID)
		{
			try
			{
				string queryStatement = "EXEC spAdminGetUsersAndRoles @UserID";
				using (SqlConnection sqlConn = new SqlConnection(connectionString))
				{
					SqlCommand cmd = new SqlCommand(queryStatement, sqlConn);
						
					cmd.Parameters.Add("@UserID", SqlDbType.NChar).Value = userID;


					DataTable userTable = new DataTable("UsersAndRoles");
					SqlDataAdapter _dap = new SqlDataAdapter(cmd);

					_dap.Fill(userTable);

					IEnumerable<CustomDBUser> result = userTable.AsEnumerable().Select(user => new CustomDBUser
					{
						ID = user.Field<string>("Id"),
						FirstName = user.Field<string>("FirstName"),
						LastName = user.Field<string>("LastName"),
						Role = user.Field<string>("Role"),
						UserName = user.Field<string>("UserName")
					});

					return result;
				}

			}
			catch (SqlException e)
			{
				Console.Error.WriteLine(e.Message);
				return null;
			}

		}
	}
}

