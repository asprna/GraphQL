using Dapper;
using Domain;
using MediatR;
using Persistence.DBConnectionFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Artists
{
	public class Details
	{
		public class Query : IRequest<Artist>
		{
			public int ArtistId { get; set; }
		}

		public class Handler : IRequestHandler<Query, Artist>
		{
			private readonly IConnectionFactory _connection;

			public Handler(IConnectionFactory connection)
			{
				_connection = connection;
			}
			public async Task<Artist> Handle(Query request, CancellationToken cancellationToken)
			{
				var connection = _connection.GetDbConnection();

				const string getArtist = @"SELECT ar.Name, ar.artistid FROM artists ar WHERE artistid = @ArtistId LIMIT 1";

				var artist = await connection.QueryAsync<Artist>(getArtist, new { request.ArtistId });

				return artist.FirstOrDefault();
			}
		}
	}
}
