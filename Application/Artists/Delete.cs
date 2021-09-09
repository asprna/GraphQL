using Dapper;
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
	public class Delete
	{
		public class Command : IRequest
		{
			public int ArtistId { get; set; }
		}

		public class Handler : IRequestHandler<Command>
		{
			private readonly IConnectionFactory _connection;

			public Handler(IConnectionFactory connection)
			{
				_connection = connection;
			}
			public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				var connection = _connection.GetDbConnection();

				const string editArtist = @"DELETE artists WHERE ArtistId = @ArtistId";

				await connection.ExecuteAsync(editArtist, request.ArtistId);

				return Unit.Value;
			}
		}
	}
}
