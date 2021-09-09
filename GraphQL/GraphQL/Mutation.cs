using Domain;
using GraphQL.GraphQL.Artists;
using HotChocolate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using HotChocolate.Subscriptions;
using System.Threading;

namespace GraphQL.GraphQL
{
	public class Mutation
	{
		public async Task<AddArtistPayload> AddArtistAsync(AddArtistInput input, [Service] IMediator mediator, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
		{
			var artist = new Artist
			{
				Name = input.Name
			};

			var id = await mediator.Send(new Application.Artists.Create.Command { Artist = artist });

			var result = await mediator.Send(new Application.Artists.Details.Query { ArtistId = id });

			await eventSender.SendAsync(nameof(Subscription.OnArtistAdded), result, cancellationToken);

			return new AddArtistPayload(result);
		}
	}
}
