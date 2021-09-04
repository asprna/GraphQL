using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Domain;
using MediatR;
using Persistence.DBConnectionFactory;

namespace Application.Albums
{
    public class List
    {
        public class Query : IRequest<List<Album>> { }

        public class Handler : IRequestHandler<Query, List<Album>>
        {
            public IConnectionFactory _connection { get; }
            public Handler(IConnectionFactory connection)
            {
                _connection = connection;
            }

            public async Task<List<Album>> Handle(Query request, CancellationToken cancellationToken)
            {
                var connection = _connection.GetDbConnection();

                const string allAlbums = "SELECT * FROM albums";

                var albums = await connection.QueryAsync<Album>(allAlbums);

                return albums.AsList();
            }
        }
    }
}