using AutoMapper;
using SchoolPlayground.Models;

namespace SchoolPlayground.Profiles
{
	public class StudentProfile : Profile
	{
		public StudentProfile()
		{
			CreateMap<Student, StudentUpsertDto>().ReverseMap();
			CreateMap<Student, StudentDto>().ReverseMap();
		}
	}
}
