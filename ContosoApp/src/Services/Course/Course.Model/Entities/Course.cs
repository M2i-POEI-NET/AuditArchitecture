using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Model.Entities
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        //public ICollection<Enrollment> Enrollments { get; set; }
    }
}
