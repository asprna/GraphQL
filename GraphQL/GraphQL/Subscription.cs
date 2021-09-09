using Domain;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL
{
	public class Subscription
	{
		[Subscribe]
		[Topic]
		public Artist OnArtistAdded([EventMessage] Artist artist) => artist;
	}
}
