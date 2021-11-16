using GraphQL.DTOs;
using GraphQL.GraphQL;
using GraphQL.GraphQL.MutationResolvers;
using GraphQL.GraphQL.MutationType;
using GraphQL.GraphQL.ObjectTypes;
using GraphQL.GraphQL.QueryTypes;
using GraphQL.Services;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Extensions
{
	public static class GraphQLServiceExtension
	{
		public static IServiceCollection AddGraphQLService(this IServiceCollection services)
		{
			services.AddGraphQLServer()
				.AddFairyBread()
				//.AddQueryType<Query>()
				.AddQueryType(q => q.Name("Query"))
				// this option will, by default, define that you want to declare everything explicitly.
				.ModifyOptions(c => c.DefaultBindingBehavior = BindingBehavior.Explicit)
				.AddTypeExtension<AlbumQueryTypeExtension>()
				.AddTypeExtension<ArtistQueryTypeExtension>()
				.AddMutationType(m => m.Name("Mutation"))
				.AddTypeExtension<ArtistMutationTypeExtension>()
				.AddTypeExtension<AuthenticationMutationTypeExtension>()
				//.AddProjections()
				.AddSubscriptionType<SubscriptionType>()
				//.AddType<AlbumType>()
				//.AddType<ArtistType>()
				//.AddType<LoginType>()
				//.AddFiltering()
				//.AddSorting()
				.AddInMemorySubscriptions()
				.AddAuthorization()
				//.AddFluentValidation()
				;

			services.AddErrorFilter<ErrorFilterService>();
			return services;
		}
	}
}
