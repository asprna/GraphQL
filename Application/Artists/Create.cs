using Application.Core;
using Domain;
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
	public class Create
	{
		public class Command : IRequest<Result<long>>
		{
			public Artist Artist { get; set; }
		}

		public class Handler : IRequestHandler<Command, Result<long>>
		{
			private readonly DataContext _dataContext;

			public Handler(DataContext dataContext)
			{
				_dataContext = dataContext;
			}

			public async Task<Result<long>> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var artist = _dataContext.Artists.Add(request.Artist);

					var result = await _dataContext.SaveChangesAsync() > 0;

					if (!result) return Result<long>.Failure("Failed to create artist");

					return Result<long>.Success(artist.Entity.ArtistId);
				}
				catch
				{
					return Result<long>.Failure("Failed to create artist");
				}
				
			}
		}
	}
}
