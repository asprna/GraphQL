using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IAuthenticationService
	{
		Task<UserDto> LoginAsync(LoginDto loginDto);
		Task<UserDto> Register(RegisterDto registerDto);
		Task<UserDto> RefreshToken();
	}
}
