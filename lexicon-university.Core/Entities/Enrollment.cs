using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexicon_university.Core.Entities
{
    internal class Enrollment
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int Grade { get; set; }
    }
}
