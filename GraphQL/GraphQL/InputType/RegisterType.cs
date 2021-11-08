using GraphQL.DTOs;
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
}
