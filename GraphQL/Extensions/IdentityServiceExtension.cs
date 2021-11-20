using Application.Interfaces;
using Domain;
using GraphQL.Models;
using GraphQL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Extensions
{
	public static class IdentityServiceExtension
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentityCore<User>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
			})
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<DataContext>()
				.AddSignInManager<SignInManager<User>>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt =>
				{
					opt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("TokenSetting").GetValue<string>("Key"))),
						ValidateIssuer = true,
						ValidIssuer = configuration.GetSection("TokenSetting").GetValue<string>("Issuer"),
						ValidateAudience = true,
						ValidAudience = configuration.GetSection("TokenSetting").GetValue<string>("Audience")
					};
				});
			services.AddAuthorization();

			services.Configure<TokenSettings>(configuration.GetSection("TokenSetting"));

			services.AddScoped<TokenServices>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();

			return services;
		}
	}
}
