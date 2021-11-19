using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Albums
{
	/// <summary>
	/// Query handler for Album details.
	/// </summary>
	public class Details
	{
		public class Query : IRequest<Result<Album>>
		{
			public int AlbumId { get; set; }
		}

		public class Handler : IRequestHandler<Query, Result<Album>>
		{
			private readonly DataContext _dataContext;

			public Handler(DataContext dataContext)
			{
				_dataContext = dataContext;
			}

			public async Task<Result<Album>> Handle(Query request, CancellationToken cancellationToken)
			{
				try
				{
					var album = await _dataContext.Albums.SingleAsync(id => id.AlbumId == request.AlbumId);
					await _dataContext.Entry(album).Reference(a => a.Artist).LoadAsync();
					await _dataContext.Entry(album).Collection(a => a.Tracks).LoadAsync();
					return Result<Album>.Success(album);
				}
				catch
				{
					return Result<Album>.Failure("Failed get Album details");
				}
			}
		}
	}
}
