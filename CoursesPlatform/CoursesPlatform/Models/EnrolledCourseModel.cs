using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class EnrolledCourseModel
    {
        public int CourseID { get; set; }
        public DateTime Registration { get; set; }

        public TraineeModel Trainee { get; set; }

    }
}