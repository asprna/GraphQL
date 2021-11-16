using MediatR;
using Persistence;
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
			private readonly DataContext _dataContext;

			public Handler(DataContext dataContext)
			{
				_dataContext = dataContext;
			}

			public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				var artist = _dataContext.Artists.FindAsync(request.ArtistId);

				_dataContext.Remove(artist);

				await _dataContext.SaveChangesAsync();

				return Unit.Value;
			}
		}
	}
}
