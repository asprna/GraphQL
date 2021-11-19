using Domain;
using HotChocolate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.QueryResolvers
{
	public class AlbumQueryResolver
	{
		public async Task<List<Album>> GetAlbums([Service] IMediator mediator)
		{
			var result = await mediator.Send(new Application.Albums.List.Query());
			return result;
		}

		public async Task<Album> GetAlbumsById(int albumId, [Service] IMediator mediator)
		{
			var result = await mediator.Send(new Application.Albums.Details.Query { AlbumId = albumId });
			return result;
		}
	}
}
