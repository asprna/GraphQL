using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.DTOs
{
	public record LoginDto (string Email, string Password);

	public class LoginDtoValidator : AbstractValidator<LoginDto>
	{
		public LoginDtoValidator()
		{
			RuleFor(input => input.Email).EmailAddress().WithMessage("Email is invalid");
			RuleFor(input => input.Password).NotEmpty().MinimumLength(5).WithMessage("Password is invalid");
		}
	}
}
