namespace lexicon_university.Web.Models.ViewModels
{
    public class StudentDetailsViewModel
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int Attending { get; set; }

        public IEnumerable<Course> Courses { get; set; }

    }
}
