using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.ObjectTypes
{
	public class SubscriptionType : ObjectType<Subscription>
	{
		protected override void Configure(IObjectTypeDescriptor<Subscription> descriptor)
		{
			descriptor.Field(t => t.OnArtistAdded(default))
				.Type<ArtistType>();
		}
	}
}
