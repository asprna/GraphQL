using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.DTOs
{
	public record RegisterDto (string DisplayName, string Email, string Password, string UserName);

	public class RegisterDtoValidator : AbstractValidator<RegisterDto>
	{
		public RegisterDtoValidator()
		{
			RuleFor(input => input.DisplayName).NotEmpty().WithMessage("Display name should not be empty");
			RuleFor(input => input.Email).EmailAddress().WithMessage("Email is invalid");
			RuleFor(input => input.Password).Matches(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$").WithMessage("Password is invalid");
			RuleFor(input => input.UserName).NotEmpty().WithMessage("User name should not be empty");
		}
	}
}
