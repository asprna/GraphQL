using Domain;
using FluentValidation;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.GraphQL.InputType
{
	public class ArtistInputType : InputObjectType<Artist>
	{
		protected override void Configure(IInputObjectTypeDescriptor<Artist> descriptor)
		{
			descriptor.Description("Artist Input Type");
			descriptor.Field(f => f.ArtistId).Name("ArtistID").Type<LongType>();
			descriptor.Field(f => f.Name).Name("Name").Type<StringType>();
		}
	}

	public class ArtistValidator : AbstractValidator<Artist>
	{
		public ArtistValidator()
		{
			RuleFor(input => input.ArtistId).NotEmpty().GreaterThan(0).WithMessage("Artist Id is invalid");
			RuleFor(input => input.Name).NotEmpty().WithMessage("Name is invalid");
		}
	}
}
