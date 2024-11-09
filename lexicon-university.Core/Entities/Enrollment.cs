using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexicon_university.Core.Entities
{
    public class Enrollment
    {
        public int CourseId { get; set; }
        public int Grade { get; set; }

        //Convention 4 Add foreign key. 
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
