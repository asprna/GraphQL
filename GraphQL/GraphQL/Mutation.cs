using Domain;
using GraphQL.GraphQL.Artists;
using HotChocolate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;

namespace GraphQL.GraphQL
{
	public class Mutation
	{
		public async Task<AddArtistPayload> AddArtistAsync(AddArtistInput input, [Service] IMediator mediator)
		{
			var artist = new Artist
			{
				Name = input.Name
			};

			var id = await mediator.Send(new Application.Artists.Create.Command { Artist = artist });

			var result = await mediator.Send(new Application.Artists.Details.Query { ArtistId = id });

			return new AddArtistPayload(result);
		}
	}
}
