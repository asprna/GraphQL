using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Albums
{
    /// <summary>
    /// Query handler for getting all the albums.
    /// </summary>
	public class List
    {
        public class Query : IRequest<Result<List<Album>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Album>>>
        {
            private readonly DataContext _dataContext;

            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Result<List<Album>>> Handle(Query request, CancellationToken cancellationToken)
            {
				try
				{
                    return Result<List<Album>>.Success(await _dataContext.Albums.ToListAsync());
                }
				catch
				{
                    return Result<List<Album>>.Failure("Unable get Albums");
				}
            }
        }
    }
}