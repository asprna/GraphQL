using Domain;
using GraphQL.DTOs;
using GraphQL.Services;
using HotChocolate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace GraphQL.GraphQL.MutationResolvers
{
	public class AuthenticationMutateResolvers
	{
		public async Task<UserDto> LoginAsync(LoginDto loginDto,
			[Service] UserManager<User> userManager,
			[Service] SignInManager<User> signInManager,
			[Service] TokenServices tokenService,
			[Service] IHttpContextAccessor httpContextAccessor)
		{
			var user = await userManager.FindByEmailAsync(loginDto.Email);
			
			if (user == null) 
			{
				throw new Exception("User Credentials are invalid");
			}

			var roles = await userManager.GetRolesAsync(user);

			var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (result.Succeeded)
			{
				await SetRefereshToken(user, httpContextAccessor, tokenService, userManager);
				var token = tokenService.CreateToken(user, roles.ToList());
				return new UserDto { DisplayName = user.DisplayName, Token = token, Username = user.UserName };
			}

			throw new Exception("Login Failed");
		}

		public async Task<UserDto> Register(RegisterDto registerDto,
			[Service] UserManager<User> userManager,
			[Service] TokenServices tokenService,
			[Service] IHttpContextAccessor httpContextAccessor)
		{
			if (await userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
			{
				throw new Exception("Email taken");
			}

			if (await userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName))
			{
				throw new Exception("Username taken");
			}

			var user = new User
			{
				DisplayName = registerDto.DisplayName,
				Email = registerDto.Email,
				UserName = registerDto.UserName
			};

			var result = await userManager.CreateAsync(user, registerDto.Password);

			await userManager.AddToRoleAsync(user, "Customer");

			var roles = await userManager.GetRolesAsync(user);

			if (result.Succeeded)
			{
				await SetRefereshToken(user, httpContextAccessor, tokenService, userManager);
				return new UserDto
				{
					DisplayName = user.DisplayName,
					Token = tokenService.CreateToken(user, roles.ToList()),
					Username = user.UserName
				};
			}

			throw new Exception("Unable to register the user");
		}

		public async Task<UserDto> RefreshToken([Service] IHttpContextAccessor httpContextAccessor,
			[Service] UserManager<User> userManager,
			[Service] TokenServices tokenService)
		{
			var refreshToken = httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
			var user = await userManager.Users.Include(r => r.RefreshTokens)
				.FirstOrDefaultAsync(x => x.Email == httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email));

			if (user == null)
			{
				throw new Exception("Not Authorize");
			}

			var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

			if (oldToken != null && !oldToken.IsActive)
			{
				throw new Exception("Invalid Token");
			}

			var roles = await userManager.GetRolesAsync(user);

			return new UserDto
			{
				DisplayName = user.DisplayName,
				Token = tokenService.CreateToken(user, roles.ToList()),
				Username = user.UserName
			};
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
