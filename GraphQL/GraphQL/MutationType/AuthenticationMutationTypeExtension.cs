using FairyBread;
using GraphQL.GraphQL.InputType;
using GraphQL.GraphQL.MutationResolvers;
using GraphQL.GraphQL.ObjectTypes;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.MutationType
{
	public class AuthenticationMutationTypeExtension : ObjectTypeExtension
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("Mutation");

			descriptor.Field("Login")
				.ResolveWith<AuthenticationMutateResolvers>(f => f.LoginAsync(default, default))
				.Argument("loginDto", a => a.Type<LoginType>())
				.Type<UserType>()
				.Name("Login");

			descriptor.Field("Register")
				.ResolveWith<AuthenticationMutateResolvers>(f => f.Register(default, default))
				.Argument("registerDto", a => a.Type<RegisterType>())
				.Type<UserType>()
				.Name("Register");

			descriptor.Field("RefreshToken")
				.ResolveWith<AuthenticationMutateResolvers>(f => f.RefreshToken(default))
				.Type<UserType>()
				.Name("RefreshToken")
				.Authorize();
		}
	}
}
