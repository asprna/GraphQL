using GraphQL.GraphQL;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.DBConnectionFactory;
using Microsoft.Extensions.Logging;
using Application.Albums;
using GraphQL.Server.Ui.Voyager;
using GraphQL.GraphQL.ObjectTypes;
using Domain;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Microsoft.EntityFrameworkCore;
using GraphQL.Services;
using GraphQL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GraphQL.GraphQL.MutationType;
using GraphQL.GraphQL.QueryTypes;
using GraphQL.Extensions;
using GraphQL.DTOs;
using FluentValidation;
using Application.Helper;

namespace GraphQL
{
	public class Startup
	{
		private IConfiguration _configuration { get; }

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMediatR(typeof(List.Handler).Assembly);
			services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
			services.AddGraphQLService();

			//Register AutoMapper
			services.AddAutoMapper(typeof(MappingProfile).Assembly);

			services.AddDbContext<DataContext>(opt =>
			{
				opt.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddIdentityCore<User>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
			})
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<DataContext>()
				.AddSignInManager<SignInManager<User>>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt => {
					opt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenSetting").GetValue<string>("Key"))),
						ValidateIssuer = true,
						ValidIssuer = _configuration.GetSection("TokenSetting").GetValue<string>("Issuer"),
						ValidateAudience = true,
						ValidAudience = _configuration.GetSection("TokenSetting").GetValue<string>("Audience")
					};
				});
			services.AddAuthorization();

			services.Configure<TokenSettings>(_configuration.GetSection("TokenSetting"));
			
			services.AddTransient<IConnectionFactory>(s => new SqliteConnectionFactory(_configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped<TokenServices>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseWebSockets();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGraphQL();

				endpoints.MapGet("/", context =>
				{
					return Task.Run(() => context.Response.Redirect("/graphql"));
				});
			});

			app.UseGraphQLVoyager(new VoyagerOptions(){
                GraphQLEndPoint = "/graphql"
            }, "/graphql-voyager");
		}
	}
}
