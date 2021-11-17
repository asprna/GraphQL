using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.GraphQL;
using GraphQL.GraphQL.InputType;
using GraphQL.GraphQL.MutationResolvers;
using GraphQL.GraphQL.ObjectTypes;
using HotChocolate.Types;

namespace GraphQL.GraphQL.MutationType
{
	public class ArtistMutationTypeExtension : ObjectTypeExtension
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("Mutation");

			descriptor.Field("AddArtist")
				.ResolveWith<ArtistMutateResolvers>(f => f.AddArtistAsync(default, default, default, default))
				.Argument("name", a => a.Type<StringType>())
				.Type<ArtistType>()
				.Name("AddArtist")
				.Authorize(new[] { "Admin", "Artist", "Manager" });

			descriptor.Field("EditArtist")
				.ResolveWith<ArtistMutateResolvers>(f => f.EditArtistAsync(default, default, default))
				.Argument("artist", a => a.Type<ArtistInputType>())
				.Type<ArtistType>()
				.Name("EditArtist")
				.Authorize(new[] { "Admin", "Artist", "Manager" });
		}
	}
}
