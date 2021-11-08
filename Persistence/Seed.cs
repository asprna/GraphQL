using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
	public class Seed
	{
		public static async Task SeedData(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.Roles.Any())
			{
				var roles = new List<IdentityRole>
				{
					new Role{Name = "Admin" },
					new Role{Name = "Artist" },
					new Role{Name = "Customer"},
					new Role{Name = "Manager"}
				};

				foreach(var role in roles)
				{
					await roleManager.CreateAsync(role);
				}
			}

			if (!userManager.Users.Any())
			{
				var users = new List<User>
				{
					new User{DisplayName = "Bob", UserName = "bob", Email = "bob@test.com"},
					new User{DisplayName = "Tom", UserName = "tom", Email = "tom@test.com"},
					new User{DisplayName = "Jane", UserName = "jane", Email = "jane@test.com"}
				};

				foreach(var user in users)
				{
					await userManager.CreateAsync(user, "Pa$$w0rd");
					if (user.DisplayName == "Bob")
					{
						await userManager.AddToRoleAsync(user, "Admin");
					} else
					{
						await userManager.AddToRoleAsync(user, "Artist");
					}
				}
			}
		}
	}
}
