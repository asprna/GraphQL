using Domain;
using GraphQL.Services;
using HotChocolate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.DTOs;

namespace GraphQL.GraphQL.MutationResolvers
{
	public class AuthenticationMutateResolvers
	{
		public async Task<UserDto> LoginAsync(LoginDto loginDto,
			[Service] IAuthenticationService authenticationService)
		{
			return await authenticationService.LoginAsync(loginDto);
		}

		public async Task<UserDto> Register(RegisterDto registerDto,
			[Service] IAuthenticationService authenticationService)
		{
			return await authenticationService.Register(registerDto);
		}

		public async Task<Domain.DTOs.UserDto> RefreshToken([Service] IAuthenticationService authenticationService)
		{
			return await authenticationService.RefreshToken();
		}

		private async Task SetRefereshToken(User user, IHttpContextAccessor httpContextAccessor, TokenServices tokenService, UserManager<User> userManager)
		{
			if (httpContextAccessor.HttpContext != null)
			{
				var refreshToken = tokenService.GetRefreshToken();

				user.RefreshTokens.Add(refreshToken);
				await userManager.UpdateAsync(user);

				var cookieOption = new CookieOptions
				{
					HttpOnly = true,
					Expires = DateTime.UtcNow.AddDays(7)
				};

				httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOption);
			}
		}
	}
}
