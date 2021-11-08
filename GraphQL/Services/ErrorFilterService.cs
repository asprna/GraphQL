using HotChocolate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Services
{
	public class ErrorFilterService : IErrorFilter
	{
		private readonly IWebHostEnvironment _env;

		public ErrorFilterService(IWebHostEnvironment env)
		{
			_env = env;
		}

		public IError OnError(IError error)
		{
			if (!_env.IsDevelopment())
			{
				error = error.RemoveExtensions().RemoveLocations().RemovePath();
			}
			return error.WithMessage(error.Code.Equals("FairyBread_ValidationError") ? error.Message : error.Exception?.Message ?? error.Message);
		}
	}
}
