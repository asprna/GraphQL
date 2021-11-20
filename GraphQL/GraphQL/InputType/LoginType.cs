using FluentValidation;
using Domain.DTOs;
using HotChocolate.Types;

namespace GraphQL.GraphQL.InputType
{
	public class LoginType : InputObjectType<LoginDto>
	{
		protected override void Configure(IInputObjectTypeDescriptor<LoginDto> descriptor)
		{
			descriptor.Description("Login Details");
			descriptor.Field(f => f.Email).Name("Email").Type<StringType>();
			descriptor.Field(f => f.Password).Name("Password").Type<StringType>();
		}
	}

	public class LoginDtoValidator : AbstractValidator<LoginDto>
	{
		public LoginDtoValidator()
		{
			RuleFor(input => input.Email).EmailAddress().WithMessage("Email is invalid");
			RuleFor(input => input.Password).NotEmpty().MinimumLength(5).WithMessage("Password is invalid");
		}
	}
}
