using AutoMapper;

namespace lexicon_university.Web.AutoMapperConfig
{
    public class UniversityMapping : Profile
    {
        public UniversityMapping()
        {
            CreateMap<StudentCreateViewModel, Student>()
                .ForMember(
                    dest => dest.Avatar,
                    opt => opt.MapFrom(src => "https://thispersondoesnotexist.com/") // Set static avatar URL
                )
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => new Address
                    {
                        Street = src.Street,
                        ZipCode = src.ZipCode,
                        City = src.City
                    })
                )
                .ForMember(
                    dest => dest.Enrollments,
                    opt => opt.MapFrom(src => src.SelectedCourses.Select(courseId => new Enrollment
                    {
                        CourseId = courseId,
                        Grade = new Random().Next(1, 6) // Generates a random grade between 1 and 5
                    }).ToList())
                   );

            CreateMap<Student, StudentIndexViewModel>()
                .ForMember(
                dest => dest.CourseInfos,
                opts => opts.MapFrom(src => src.Enrollments.Select(e => new CourseInfo
                {
                    Grade = e.Grade,
                    CourseName = e.Course.Title
                })))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));


            CreateMap<Student, StudentDetailsViewModel>()
               .ForMember(
               dest => dest.Attending,
               from => from.MapFrom(s => s.Courses.Count))
               .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
               .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
               .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode));
        }
    }
}
