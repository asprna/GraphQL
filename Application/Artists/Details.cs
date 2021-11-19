using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Artists
{
	/// <summary>
	/// Query handler for artist details.
	/// </summary>
	public class Details
	{
		public class Query : IRequest<Result<Artist>>
		{
			public long ArtistId { get; set; }
		}

		public class Handler : IRequestHandler<Query, Result<Artist>>
		{
			private readonly DataContext _dataContext;

			public Handler(DataContext dataContext)
			{
				_dataContext = dataContext;
			}

			public async Task<Result<Artist>> Handle(Query request, CancellationToken cancellationToken)
			{
				try
				{
					var artist = await _dataContext.Artists.SingleOrDefaultAsync(a => a.ArtistId == request.ArtistId, cancellationToken);

					if (artist == null) return Result<Artist>.Failure("Failed to get details about Artist");

					await _dataContext.Entry(artist).Collection(a => a.Albums).LoadAsync();

					return Result<Artist>.Success(artist);
				}
				catch
				{
					return Result<Artist>.Failure("Failed to get details about Artist");
				}
				
			}
		}
	}
}
