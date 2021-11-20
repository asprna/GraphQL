using GraphQL.GraphQL.ObjectTypes;
using GraphQL.GraphQL.QueryResolvers;
using HotChocolate.Types;

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

			descriptor
				.Field("GetAlbum")
				.ResolveWith<AlbumQueryResolver>(r => r.GetAlbumsById(default, default))
				.Type<AlbumType>()
				.Argument("albumId", a => a.Type<IntType>())
				.Name("GetAlbum");
		}
	}
}
