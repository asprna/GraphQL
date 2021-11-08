using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.DTOs
{
	public record UserDto
	{
		public string DisplayName { get; set; }
		public string Token { get; set; }
		public string Username { get; set; }
	}
}
