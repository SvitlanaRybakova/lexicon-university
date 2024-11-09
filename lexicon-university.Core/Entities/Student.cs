using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexicon_university.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }

        //Navigational property
        public Address Address { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}
