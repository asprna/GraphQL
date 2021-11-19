using Application.Interfaces;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQL.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly TokenServices _tokenService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, TokenServices tokenService, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<UserDto> LoginAsync(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);

			if (user == null)
			{
				throw new Exception("User Credentials are invalid");
			}

			var roles = await _userManager.GetRolesAsync(user);

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (result.Succeeded)
			{
				await SetRefereshToken(user);
				var token = _tokenService.CreateToken(user, roles.ToList());
				return new UserDto { DisplayName = user.DisplayName, Token = token, Username = user.UserName };
			}

			throw new Exception("Login Failed");
		}

		public async Task<UserDto> RefreshToken()
		{
			var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
			var user = await _userManager.Users.Include(r => r.RefreshTokens)
				.FirstOrDefaultAsync(x => x.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email));

			if (user == null)
			{
				throw new Exception("Not Authorize");
			}

			var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

			if (oldToken != null && !oldToken.IsActive)
			{
				throw new Exception("Invalid Token");
			}

			var roles = await _userManager.GetRolesAsync(user);

			return new UserDto
			{
				DisplayName = user.DisplayName,
				Token = _tokenService.CreateToken(user, roles.ToList()),
				Username = user.UserName
			};
		}

		public async Task<UserDto> Register(RegisterDto registerDto)
		{
			if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
			{
				throw new Exception("Email taken");
			}

			if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName))
			{
				throw new Exception("Username taken");
			}

			var user = new User
			{
				DisplayName = registerDto.DisplayName,
				Email = registerDto.Email,
				UserName = registerDto.UserName
			};

			var result = await _userManager.CreateAsync(user, registerDto.Password);

			await _userManager.AddToRoleAsync(user, "Customer");

			var roles = await _userManager.GetRolesAsync(user);

			if (result.Succeeded)
			{
				await SetRefereshToken(user);
				return new UserDto
				{
					DisplayName = user.DisplayName,
					Token = _tokenService.CreateToken(user, roles.ToList()),
					Username = user.UserName
				};
			}

			throw new Exception("Unable to register the user");
		}

		private async Task SetRefereshToken(User user)
		{
			if (_httpContextAccessor.HttpContext != null)
			{
				var refreshToken = _tokenService.GetRefreshToken();

				user.RefreshTokens.Add(refreshToken);
				await _userManager.UpdateAsync(user);

				var cookieOption = new CookieOptions
				{
					HttpOnly = true,
					Expires = DateTime.UtcNow.AddDays(7)
				};

				_httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOption);
			}
		}
	}
}
