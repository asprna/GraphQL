using FluentValidation;
using Domain.DTOs;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.InputType
{
	public class RegisterType : InputObjectType<RegisterDto>
	{
		protected override void Configure(IInputObjectTypeDescriptor<RegisterDto> descriptor)
		{
			descriptor.Description("Register User");
			descriptor.Field(f => f.Email).Name("Email").Type<StringType>();
			descriptor.Field(f => f.Password).Name("Password").Type<StringType>();
			descriptor.Field(f => f.DisplayName).Name("DisplayName").Type<StringType>();
			descriptor.Field(f => f.UserName).Name("UserName").Type<StringType>(); 
		}
	}

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
