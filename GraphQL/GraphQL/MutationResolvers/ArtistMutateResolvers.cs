using Domain;
using HotChocolate;
using HotChocolate.Subscriptions;
using MediatR;
using System;
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

			var newArtist = await mediator.Send(new Application.Artists.Details.Query { ArtistId = result.Value });

			await eventSender.SendAsync(nameof(Subscription.OnArtistAdded), newArtist.Value, cancellationToken);

			return newArtist.Value;
		}

		public async Task<Artist> EditArtistAsync(Artist artist, [Service] IMediator mediator, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(new Application.Artists.Edit.Command { Artist = artist });

			if (!result.IsSuccess) throw new Exception(result.Error);

			var updatedArtist = await mediator.Send(new Application.Artists.Details.Query { ArtistId = artist.ArtistId });

			return updatedArtist.Value;
		}

		public async Task<string> DeleteArtistAsync(long artistId, [Service] IMediator mediator, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(new Application.Artists.Delete.Command { ArtistId = artistId });

			if (!result.IsSuccess) throw new Exception(result.Error);

			return "Success";
		}
	}
}
