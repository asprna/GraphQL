using GraphQL.GraphQL.ObjectTypes;
using GraphQL.GraphQL.QueryResolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.QueryTypes
{
	public class AlbumQueryTypeExtension : ObjectTypeExtension
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("Query");

			descriptor
				.Field("GetAlbums")
				.ResolveWith<AlbumQueryResolver>(r => r.GetAlbums(default))
				.Type<ListType<AlbumType>>()
				.Name("GetAlbums");
		}
	}
}
