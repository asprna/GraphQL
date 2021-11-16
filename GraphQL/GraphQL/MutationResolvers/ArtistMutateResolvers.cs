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

			var result = await mediator.Send(new Application.Artists.Create.Command { Artist = artist });

			if (!result.IsSuccess) throw new Exception(result.Error);

			var newArtist = await mediator.Send(new Application.Artists.Details.Query { ArtistId = (int)result.Value });

			await eventSender.SendAsync(nameof(Subscription.OnArtistAdded), newArtist, cancellationToken);

			return newArtist;
		}
	}
}
