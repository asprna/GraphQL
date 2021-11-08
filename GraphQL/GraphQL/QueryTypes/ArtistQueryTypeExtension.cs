using GraphQL.GraphQL.ObjectTypes;
using GraphQL.GraphQL.QueryResolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.QueryTypes
{
	public class ArtistQueryTypeExtension : ObjectTypeExtension
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("Query");

			descriptor
				.Field("GetArtists")
				.ResolveWith<ArtistQueryResolver>(r => r.GetArtists(default))
				.Type<ListType<ArtistType>>()
				.Name("GetArtists");

			descriptor
				.Field("GetArtistById")
				.ResolveWith<ArtistQueryResolver>(r => r.GetArtistByID(default, default))
				.Type<ArtistType>()
				.Argument("artistId", a => a.Type<IntType>())
				.Name("GetArtistById");
		}
	}
}
