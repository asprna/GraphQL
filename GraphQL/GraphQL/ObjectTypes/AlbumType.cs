using Domain;
using HotChocolate.Types;

namespace GraphQL.GraphQL.ObjectTypes
{
	public class AlbumType : ObjectType<Album>
	{
		protected override void Configure(IObjectTypeDescriptor<Album> descriptor)
		{
			descriptor.Description("Contains album details");
			descriptor.Field(f => f.AlbumId).Type<IntType>().Name("AlbumId");
			descriptor.Field(f => f.Title).Type<StringType>().Name("Title");
			descriptor.Field(f => f.ArtistId).Type<IntType>().Name("ArtistId");
			
			descriptor.Field(f => f.Artist).Type<ArtistType>().Name("Artist").Description("The artist of the album");
		}
	}
}
