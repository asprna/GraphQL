using GraphQL.DTOs;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
}
