using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.Albums;
using GraphQL.Server.Ui.Voyager;
using Persistence;
using Microsoft.EntityFrameworkCore;
using GraphQL.Extensions;
using FluentValidation;
using Application.Helper;
using GraphQL.GraphQL.InputType;

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
			services.AddValidatorsFromAssemblyContaining<ArtistValidator>();
			services.AddGraphQLService();

			//Register AutoMapper
			services.AddAutoMapper(typeof(MappingProfile).Assembly);

			services.AddDbContext<DataContext>(opt =>
			{
				opt.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddIdentityService(_configuration);
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
