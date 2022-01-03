using CoursesPlatform.Database.CoursesPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Services
{
    public interface IEnrolledCourse
    {
         IEnumerable<EnrolledCourse> GetAllTrainees(int? cID = null);


    }

    public class EnrolledCourseService : IEnrolledCourse
    {
        private CoursesPlatformEntities db;

        public EnrolledCourseService()
        {
            db = new CoursesPlatformEntities();
        }
        public IEnumerable<EnrolledCourse> GetAllTrainees(int? cID=null)
        {
            return db.EnrolledCourses.Where(ec => ec.CoursesID == cID|| cID == null);

        }
    }
}