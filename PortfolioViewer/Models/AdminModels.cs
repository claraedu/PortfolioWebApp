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

		public CustomDBUser(string iD, string firstName, string lastName, string userName, string role)
		{
			ID = iD;
			FirstName = firstName;
			LastName = lastName;
			UserName = userName;
			Role = role;
		}

		public string ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }
	}

	public class Role
	{
		public Role()
		{
		}

		public Role(string iD, string name)
		{
			ID = iD;
			Name = name;
		}

		public string ID { get; set; }
		public string Name { get; set; }
	}
	
}