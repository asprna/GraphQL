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
			services.AddGraphQLServer()
				.AddQueryType<Query>()
				.AddType<AlbumType>()
				.AddType<ArtistType>()
				.AddFiltering()
				.AddSorting();
			
			services.AddTransient<IConnectionFactory>(s => new SqliteConnectionFactory(_configuration.GetConnectionString("DefaultConnection")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

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
