using Domain;
using HotChocolate;
using HotChocolate.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.MutationResolvers
{
	public class ArtistMutateResolvers
	{
		public async Task<Artist> AddArtistAsync(string name, [Service] IMediator mediator, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
		{
			var artist = new Artist
			{
				Name = name
			};

			var id = await mediator.Send(new Application.Artists.Create.Command { Artist = artist });

			var result = await mediator.Send(new Application.Artists.Details.Query { ArtistId = id });

			await eventSender.SendAsync(nameof(Subscription.OnArtistAdded), result, cancellationToken);

			return result;
		}
	}
}
