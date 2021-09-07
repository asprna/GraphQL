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

                const string allArtist = @"SELECT ar.Name, ar.artistid, al.ArtistId, al.AlbumId, al.Title
                                            FROM artists ar
                                            LEFT JOIN albums al ON al.ArtistId = ar.ArtistId
                                            ORDER BY ar.artistid";

                var artistDictonary = new Dictionary<int, Artist>();

                var artists = await connection.QueryAsync<Artist, Album, Artist>(
                            allArtist, (artist, album) =>
                            {
                                Artist artistEntry;

                                if (!artistDictonary.TryGetValue(artist.ArtistId, out artistEntry))
                                {
                                    artistEntry = artist;
                                    artist.Albums = new List<Album>();
                                    artistDictonary.Add(artistEntry.ArtistId, artistEntry);
                                }

                                artistEntry.Albums.Add(album);
                                return artistEntry;
                            },
                             splitOn: "ArtistId, ArtistId");

                return artists.Distinct().AsList();
            }
		}
    }
}
