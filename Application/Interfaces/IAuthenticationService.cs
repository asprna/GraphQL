using Domain.DTOs;
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
