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

                const string allAlbums = @"SELECT al.*, ar.*
                                           FROM albums al
                                           INNER JOIN artists ar ON ar.ArtistId = al.ArtistId";

                var albums = await connection.QueryAsync<Album, Artist, Album>(
                             allAlbums, (alburm, artist) => 
                             { 
                                 alburm.Artist = artist; 
                                 return alburm; 
                             }, 
                             splitOn: "ArtistId");

                return albums.AsList();
            }
        }
    }
}