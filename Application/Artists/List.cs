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
	public class List
	{
        public class Query : IRequest<List<Artist>> { }

        public class Handler : IRequestHandler<Query, List<Artist>>
        {
			private readonly DataContext _dataContext;

            public Handler(DataContext dataContext)
            {
				_dataContext = dataContext;
			}

            public async Task<List<Artist>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _dataContext.Artists.ToListAsync(cancellationToken);
            }
		}
    }
}
