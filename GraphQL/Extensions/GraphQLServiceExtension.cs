using GraphQL.GraphQL.MutationType;
using GraphQL.GraphQL.ObjectTypes;
using GraphQL.GraphQL.QueryTypes;
using GraphQL.Services;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Extensions
{
	public static class GraphQLServiceExtension
	{
		public static IServiceCollection AddGraphQLService(this IServiceCollection services)
		{
			services.AddGraphQLServer()
				.AddFairyBread()
				.AddQueryType(q => q.Name("Query"))
				// this option will, by default, define that you want to declare everything explicitly.
				.ModifyOptions(c => c.DefaultBindingBehavior = BindingBehavior.Explicit)
				.AddTypeExtension<AlbumQueryTypeExtension>()
				.AddTypeExtension<ArtistQueryTypeExtension>()
				.AddMutationType(m => m.Name("Mutation"))
				.AddTypeExtension<ArtistMutationTypeExtension>()
				.AddTypeExtension<AuthenticationMutationTypeExtension>()
				.AddSubscriptionType<SubscriptionType>()
				.AddInMemorySubscriptions()
				.AddAuthorization()
				;

			services.AddErrorFilter<ErrorFilterService>();
			return services;
		}
	}
}
