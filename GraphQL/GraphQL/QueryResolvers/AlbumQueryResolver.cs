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
		//[UseFiltering]
		//[UseSorting]
		public async Task<List<Album>> GetAlbums([Service] IMediator mediator)
		{
			var result = await mediator.Send(new Application.Albums.List.Query());
			return result;
		}
	}
}
