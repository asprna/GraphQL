using Domain.DTOs;
using HotChocolate.Types;

namespace GraphQL.GraphQL.ObjectTypes
{
	public class UserType : ObjectType<UserDto>
	{
		protected override void Configure(IObjectTypeDescriptor<UserDto> descriptor)
		{
			descriptor.Description("User Login Details");
			descriptor.Field(f => f.DisplayName).Type<StringType>().Name("DisplayName");
			descriptor.Field(f => f.Token).Type<StringType>().Name("Token");
			descriptor.Field(f => f.Username).Type<StringType>().Name("Username");
		}
	}
}
