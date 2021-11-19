using AutoMapper;
using Domain;

namespace Application.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Artist, Artist>();
		}
	}
}
