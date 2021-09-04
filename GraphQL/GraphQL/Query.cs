//using Application.WeatherForcast;
using Application.Albums;
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
            var result = mediator.Send(new List.Query()).Result;
            return result;
        }
	}
}
