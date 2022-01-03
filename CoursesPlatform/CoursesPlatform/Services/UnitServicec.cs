using CoursesPlatform.Database.CoursesPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CoursesPlatform.Services
{

    public interface IUnitService
    {

        int Create(Course_Units unit);
        int Update(Course_Units updatedCourse);
        IEnumerable<Course_Units> ReadCourseUnits(int courseId);
        Course_Units Get(int Id);
        Course_Units Get(int courseId, string name);
    }

    
    public class UnitService : IUnitService
    {
        private readonly CoursesPlatformEntities _db;

        public UnitService()
        {
            _db = new CoursesPlatformEntities();
        }
        public int Create(Course_Units unit)
        {
            _db.Course_Units.Add(unit);
           return  _db.SaveChanges(); 

    


        }

        public Course_Units Get(int Id)
        {
            return _db.Course_Units.Find(Id);
        }

        public Course_Units Get(int courseId, string name)
        {
            return _db.Course_Units.FirstOrDefault(u => u.CourseID == courseId && u.Name == name);
        }

        public IEnumerable<Course_Units> ReadCourseUnits(int courseId)
        {
            return _db.Course_Units.Where(u => u.CourseID == courseId);
        }

        public int Update(Course_Units updatedCourse)
        {
            throw new NotImplementedException();
        }

    }
}