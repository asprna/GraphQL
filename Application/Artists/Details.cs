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
	public class Details
	{
		public class Query : IRequest<Artist>
		{
			public long ArtistId { get; set; }
		}

		public class Handler : IRequestHandler<Query, Artist>
		{
			private readonly DataContext _dataContext;

			public Handler(DataContext dataContext)
			{
				_dataContext = dataContext;
			}

			public async Task<Artist> Handle(Query request, CancellationToken cancellationToken)
			{
				var artist = await _dataContext.Artists.SingleOrDefaultAsync(a => a.ArtistId == request.ArtistId, cancellationToken);

				if (artist == null) return null;

				await _dataContext.Entry(artist).Collection(a => a.Albums).LoadAsync();

				return artist;
			}
		}
	}
}
