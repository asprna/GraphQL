using Domain;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL.GraphQL
{
	public class Subscription
	{
		[Subscribe]
		[Topic]
		public Artist OnArtistAdded([EventMessage] Artist artist) => artist;
	}
}
