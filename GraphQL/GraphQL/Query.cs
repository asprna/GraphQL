//using Application.WeatherForcast;
using Albums = Application.Albums;
using Artists = Application.Artists;
using Domain;
using HotChocolate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Data;

namespace GraphQL.GraphQL
{
	public class Query
	{
		[UseFiltering]
		[UseSorting]
		public List<Album> GetAlbums([Service] IMediator mediator)
        {
            var result = mediator.Send(new Albums.List.Query()).Result;
            return result;
        }

		[UseFiltering]
		[UseSorting]
		public List<Artist> GetArtists([Service] IMediator mediator)
		{
			var result = mediator.Send(new Artists.List.Query()).Result;
			return result;
		}
	}
}
