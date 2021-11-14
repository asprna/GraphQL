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
				var user1 = new User { DisplayName = "Ash", UserName = "ash", Email = "ash@test.com" };
				var user2 = new User { DisplayName = "Tom", UserName = "tom", Email = "tom@test.com" };
				var user3 = new User { DisplayName = "Tim", UserName = "tim", Email = "tim@test.com" };
				var user4 = new User { DisplayName = "Bob", UserName = "bob", Email = "bob@test.com" };
				var user5 = new User { DisplayName = "Ian", UserName = "ian", Email = "ian@test.com" };

				await userManager.CreateAsync(user1, "Pa$$w0rd");
				await userManager.CreateAsync(user2, "Pa$$w0rd");
				await userManager.CreateAsync(user3, "Pa$$w0rd");
				await userManager.CreateAsync(user4, "Pa$$w0rd");
				await userManager.CreateAsync(user5, "Pa$$w0rd");

				await userManager.AddToRoleAsync(user1, "Admin");
				await userManager.AddToRoleAsync(user2, "Artist");
				await userManager.AddToRoleAsync(user3, "Manager");
				await userManager.AddToRoleAsync(user4, "Customer");
				await userManager.AddToRoleAsync(user5, "Customer");
			}
		}
	}
}
