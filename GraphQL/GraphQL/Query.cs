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
		// public List<WeatherForecast> GetWeatherForecasts([Service] IMediator mediator)
		// {
		// 	return mediator.Send(new List.Query()).Result;
		// }

		public List<Album> GetAlbums([Service] IMediator mediator)
        {
            // Platform p = new Platform(){
            //     Id = 1,
            //     Name = "Ash",
            //     LicensKey = "test"
            // };
        
            // return p;

            var result = mediator.Send(new List.Query()).Result;
            return result;
        }
	}
}
