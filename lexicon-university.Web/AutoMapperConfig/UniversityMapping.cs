using AutoMapper;

namespace lexicon_university.Web.AutoMapperConfig
{
    public class UniversityMapping :Profile
    {
        public UniversityMapping()
        {
            CreateMap<Student, StudentCreateViewModel>().ReverseMap();
            CreateMap<Student, StudentIndexViewModel>().ReverseMap();
        }
    }
}
