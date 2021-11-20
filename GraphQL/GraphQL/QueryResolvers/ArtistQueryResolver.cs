using Domain;
using HotChocolate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.QueryResolvers
{
	public class ArtistQueryResolver
	{
		public async Task<List<Artist>> GetArtists([Service] IMediator mediator)
		{
			var result = await mediator.Send(new Application.Artists.List.Query());
			
			if (!result.IsSuccess) throw new Exception(result.Error);

			return result.Value;
		}

		public async Task<Artist> GetArtistByID(int artistId, [Service] IMediator mediator)
		{
			var result = await mediator.Send(new Application.Artists.Details.Query { ArtistId = artistId });

			if (!result.IsSuccess) throw new Exception(result.Error);

			return result.Value;
		}
	}
}
