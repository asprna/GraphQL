using Application.Core;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Artists
{
	/// <summary>
	/// Command handler for artist deletion.
	/// </summary>
	public class Delete
	{
		public class Command : IRequest<Result<Unit>>
		{
			public long ArtistId { get; set; }
		}

		public class Handler : IRequestHandler<Command, Result<Unit>>
		{
			private readonly DataContext _dataContext;

			public Handler(DataContext dataContext)
			{
				_dataContext = dataContext;
			}

			public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
			{
				try
				{
					var artist = await _dataContext.Artists.FindAsync(request.ArtistId);

					if (artist == null) return Result<Unit>.Failure("Failed to delete Artist");

					_dataContext.Remove(artist);

					var result = await _dataContext.SaveChangesAsync() > 0;

					if (!result) return Result<Unit>.Failure("Failed to delete Artist");

					return Result<Unit>.Success(Unit.Value);
				}
				catch
				{
					return Result<Unit>.Failure("Failed to delete Artist");
				}
				
			}
		}
	}
}
