using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lexicon_university.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //Navigational properties
        public ICollection<Student> Students { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}