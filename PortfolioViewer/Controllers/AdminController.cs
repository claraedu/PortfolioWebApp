using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using PortfolioViewer.Interfaces;
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

		IPortfolioRepository _PortfolioRepository;

		JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
		{
			NullValueHandling = NullValueHandling.Ignore,
			Formatting = Formatting.Indented
		};

		public AdminController(IPortfolioRepository portfolioRepository)
		{
			_PortfolioRepository = portfolioRepository;
		}

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

			IEnumerable<CustomDBUser> users = SQLController.adminListUsersAndRoles(user.Id);

			return View(users);
        }

		public string NoCanDoHombre()
		{
			return "Im sorry, No can do, Hombre.";
		}


		public ActionResult UserInfo(string userID)
		{
			var user = UserManager.FindById(userID);

			var roles = SQLController.adminListRoles();

			CustomDBUser userInfo = SQLController.adminGetUserInfo(userID);

			ViewData.Add("user", userInfo);
			ViewData.Add("roles",roles);
			ViewData.Add("portfolios", _PortfolioRepository.GetPortfolios(user.CustomerID));


			return PartialView("_PartialUserInfo",userInfo);
		}

		[HttpPost]
		public string UpdateUser(CustomDBUser userInfo)
		{
			string success = SQLController.adminUpdateUserInfo(userInfo);
			return JsonConvert.SerializeObject(success);
		}

	}

	class SQLController
	{

		private static string connectionString = "server=(local)\\SQLEXPRESS;database=Portfolio;integrated Security=SSPI;MultipleActiveResultSets=true";

		public static IEnumerable<CustomDBUser> adminListUsersAndRoles(string userID)
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
						CurrentRole = new Role { ID = user.Field<string>("RoleId"), Name = user.Field<string>("Role") }
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

		public static CustomDBUser adminGetUserInfo(string userID)
		{
			try
			{
				string queryStatement = "EXEC spAdminGetUserInfo @UserID";
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
						CurrentRole = new Role { ID = user.Field<string>("RoleId"), Name = user.Field<string>("Role") },
						UserName = user.Field<string>("UserName"),
						Email = user.Field<string>("Email"),
						Phone = user.Field<string>("PhoneNumber")
					});

					return result.First();
				}

			}
			catch (SqlException e)
			{
				Console.Error.WriteLine(e.Message);
				return null;
			}
		}

		public static string adminUpdateUserInfo(CustomDBUser user)
		{
			try
			{
				string updateUserStatement = "UPDATE AspNetUsers SET Email = @Email, PhoneNumber = @Phone WHERE Id=@UserId";
				string updateRoleStatement = "UPDATE AspNetUserRoles SET RoleId = @Role WHERE UserId = @UserId";

				using (SqlConnection sqlConn = new SqlConnection(connectionString))
				{
					sqlConn.Open();
					SqlCommand cmd = new SqlCommand(updateUserStatement, sqlConn);
					cmd.Parameters.Add("@Email", SqlDbType.NChar).Value = user.Email;
					cmd.Parameters.Add("@Phone", SqlDbType.NChar).Value = user.Phone;
					cmd.Parameters.Add("@UserId", SqlDbType.NChar).Value = user.ID;
					cmd.ExecuteReader();


					SqlCommand cmd1 = new SqlCommand(updateRoleStatement, sqlConn);
					cmd1.Parameters.Add("@Role", SqlDbType.NChar).Value = user.CurrentRole.ID;
					cmd1.Parameters.Add("@UserId", SqlDbType.NChar).Value = user.ID;
					cmd1.ExecuteReader();
				}
				return "success";
			}
			catch (SqlException e)
			{
				return e.ToString();
			}
		}

		public static string adminChangePortfolioPermissions(CustomDBUser user)
		{
			try
			{
				string updateUserStatement = "UPDATE AspNetUsers SET Email = @Email, PhoneNumber = @Phone WHERE Id=@UserId";
				
				using (SqlConnection sqlConn = new SqlConnection(connectionString))
				{
					sqlConn.Open();
					SqlCommand cmd = new SqlCommand(updateUserStatement, sqlConn);
					cmd.Parameters.Add("@Email", SqlDbType.NChar).Value = user.Email;
					cmd.Parameters.Add("@Phone", SqlDbType.NChar).Value = user.Phone;
					cmd.Parameters.Add("@UserId", SqlDbType.NChar).Value = user.ID;
					cmd.ExecuteReader();

				}
				return "success";
			}
			catch (SqlException e)
			{
				return e.ToString();
			}
		}

		public static IEnumerable<Role> adminListRoles()
		{
			try
			{
				string queryStatement = "EXEC spGetRoleList";
				using (SqlConnection sqlConn = new SqlConnection(connectionString))
				{
					SqlCommand cmd = new SqlCommand(queryStatement, sqlConn);
					
					DataTable roleTable = new DataTable("Roles");
					SqlDataAdapter _dap = new SqlDataAdapter(cmd);

					_dap.Fill(roleTable);

					IEnumerable<Role> roles = roleTable.AsEnumerable().Select(role => new Role
					{
						ID = role.Field<string>("Id"),
						Name = role.Field<string>("Name")
					});

					return roles;
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

