using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
	public class Edit
	{
		public class Command : IRequest
		{
			public Artist Artist { get; set; }
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
				var artist = await _dataContext.Artists.SingleAsync(id => id.ArtistId == request.Artist.ArtistId);

				artist.Name = request.Artist.Name;

				await _dataContext.SaveChangesAsync();

				return Unit.Value;
			}
		}
	}
}
