using Domain;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.ObjectTypes
{
	public class ArtistType : ObjectType<Artist>
	{
		protected override void Configure(IObjectTypeDescriptor<Artist> descriptor)
		{
			descriptor.Description("Contains Artist details");

			descriptor
				.Field(p => p.Albums)
				.Description("Albums belong to artist");
		}
	}
}
