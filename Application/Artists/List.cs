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
	public class List
	{
        public class Query : IRequest<List<Artist>> { }

        public class Handler : IRequestHandler<Query, List<Artist>>
        {
            public IConnectionFactory _connection { get; }
            public Handler(IConnectionFactory connection)
            {
                _connection = connection;
            }

            public async Task<List<Artist>> Handle(Query request, CancellationToken cancellationToken)
            {
                var connection = _connection.GetDbConnection();

                const string allArtist = @"SELECT * FROM artists";

                var albums = await connection.QueryAsync<Artist>(allArtist);

                return albums.AsList();
            }
		}
    }
}
