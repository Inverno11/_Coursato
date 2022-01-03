using CoursesPlatform.Database.CoursesPlatform;
using System.Collections.Generic;
using System.Linq;

namespace CoursesPlatform.Services
{

    public interface ITrainerService
    {
        int Add(Trainer trainer);
        bool Delete(int ID);

        int Edit(Trainer trainer);
        Trainer GetTrainerByID(int ID);

        IEnumerable<Trainer> ReadAllTrainer();



    }
    public class TrainerService : ITrainerService
    {

        CoursesPlatformEntities db;

        public TrainerService()
        {
            db = new CoursesPlatformEntities();
        }
        public int Add(Trainer trainer)
        {

            string name = trainer.Name.ToLower();

            bool IsExist = db.Trainers.Where(tr => tr.Name == name).Any();

            if (IsExist)
            {
                return -2;
            }
            else
            {
                db.Trainers.Add(trainer);
                return db.SaveChanges(); 
            }

        }

        public bool Delete(int ID)
        {

            Trainer trainer = GetTrainerByID(ID);
            if (trainer != null)
            {
                db.Trainers.Remove(trainer);
                return db.SaveChanges() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public int Edit(Trainer trainer)
        {

            if (db.Trainers.Where(tr=>tr.Name == trainer.Name).Any())
            {
                return -3; 
            }
            else
            {
                db.Trainers.Attach(trainer);
                return db.SaveChanges();

            }




        }

        public Trainer GetTrainerByID(int ID)
        {
            return  db.Trainers.Find(ID);
        }

        public IEnumerable<Trainer> ReadAllTrainer()
        {


          return  db.Trainers.ToList();

        }
    }
}