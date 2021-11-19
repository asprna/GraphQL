using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
	public record RegisterDto (string DisplayName, string Email, string Password, string UserName);
}
