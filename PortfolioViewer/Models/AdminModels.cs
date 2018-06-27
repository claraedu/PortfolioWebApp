using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Models
{

	public class CustomDBUser
	{
		public CustomDBUser()
		{
		}

		public CustomDBUser(string iD, string firstName, string lastName, string userName, Role role)
		{
			ID = iD;
			FirstName = firstName;
			LastName = lastName;
			UserName = userName;
			CurrentRole = role;
		}

		public string ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public Role CurrentRole { get; set; }
	}

	public class Role
	{
		public Role()
		{
		}

		public Role(string id, string name)
		{
			ID = id;
			Name = name;
		}

		public string ID { get; set; }
		public string Name { get; set; }
	}

	public class PortfolioPermissions
	{

	}
	
}