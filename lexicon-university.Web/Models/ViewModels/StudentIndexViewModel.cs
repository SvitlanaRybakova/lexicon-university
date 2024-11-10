namespace lexicon_university.Web.Models.ViewModels
{
    public class StudentIndexViewModel
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }

        public IEnumerable<CourseInfo> CourseInfos { get; set; } = new List<CourseInfo>();

        public class CourseInfo
        {
            public int Grade { get; set; }
            public string CourseName { get; set; }
        }
    }
}
