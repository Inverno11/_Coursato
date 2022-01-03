using CoursesPlatform.Database.CoursesPlatform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesPlatform.Services
{

    interface ICourseService
    {
        int Add(Course courseData);
        IEnumerable<Course> GetAllCourse(string Query = null ,int? TrainerID = null , int? CategoryID=null);
        Course GetCourseByID(int ID);

        int Edit(Course course);

    }


    public class CourseService : ICourseService
    {
        public CoursesPlatformEntities db;

        public CourseService()
        {
            db = new CoursesPlatformEntities();
        }



        public int Add(Course courseData)
        {
            courseData.Creation = DateTime.Now;
            db.Courses.Add(courseData);

            return db.SaveChanges();

        }

        public IEnumerable<Course> GetAllCourse(string Query = null, int? TrainerID = null, int? CategoryID = null)
        {

            return db.Courses.Where(co => (TrainerID == null || co.TrainerID == TrainerID)

                && (CategoryID == null || co.CategoryID == CategoryID) &&
                    
                (Query == null || co.Name.Contains(Query))).ToList();

        }

        public Course GetCourseByID(int ID)
        {

           return  db.Courses.Find(ID);

        }

        public int UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public int Edit (Course course)
        {
           
                db.Courses.Attach(course);
                db.Entry(course).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
        }


    }
}