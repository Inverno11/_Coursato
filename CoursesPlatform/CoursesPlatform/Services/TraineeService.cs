using CoursesPlatform.Database.CoursesPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Services
{
    public interface ITraineeService
    {


        Trainee Add(Trainee trainee);

    }
    public class TraineeService : ITraineeService
    {
        private CoursesPlatformEntities db;
        public TraineeService()
        {
            db = new CoursesPlatformEntities();

        }

        public Trainee Add(Trainee trainee)
        {
            db.Trainees.Add(trainee);
            int addResult = db.SaveChanges();
            if (addResult >= 1)
            {
                return trainee;
            }
            else
            { return null; } 
        }
    }
}