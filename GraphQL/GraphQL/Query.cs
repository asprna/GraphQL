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

namespace GraphQL.GraphQL
{
	public class Query
	{
		public List<Album> GetAlbums([Service] IMediator mediator)
        {
            var result = mediator.Send(new Albums.List.Query()).Result;
            return result;
        }

		public List<Artist> GetArtists([Service] IMediator mediator)
		{
			var result = mediator.Send(new Artists.List.Query()).Result;
			return result;
		}
	}
}
