using AutoMapper;
using CoursesPlatform.App_Start;
using CoursesPlatform.Database.CoursesPlatform;
using CoursesPlatform.Models;
using CoursesPlatform.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CoursesPlatform.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class TrainerController : Controller
    {
        // GET: Admin/Trainer
        public TrainerService Service { set; get; }
        public CourseService courseService { get; set; }

        private readonly IMapper mapper; 
        public TrainerController()
        {
            Service = new TrainerService();
            mapper = AutoMapperConfig.Mapper; 
        }
        public ActionResult Index()
        {

            List<Trainer> trainers = Service.ReadAllTrainer().ToList();
            List<TrainerModel> trainersModel = mapper.Map<List<TrainerModel>>(trainers);
            return View(trainersModel);
        }

        // GET: Admin/Trainer/Details/5
        public ActionResult Details()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Details(int TrainerID)
        {
            var trainerCourse = courseService.GetAllCourse(null , TrainerID, null);
            var trainerCourseDTO = mapper.Map<CourseModel>(trainerCourse);

            return View(trainerCourseDTO);
        }


        // GET: Admin/Trainer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Trainer/Create
        [HttpPost]
        public ActionResult Create(TrainerModel trainerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Trainer trainer = mapper.Map<Trainer>(trainerModel);
                    trainer.Creation = DateTime.Now;

                    int Create = Service.Add(trainer);

                    if (Create >= 1)
                    {
                        return RedirectToAction("Index");

                    }

                    else if (Create == -2)
                    {
                        ViewBag.Message = " Trainer is already exist";

                    }
                    else
                    {
                        ViewBag.Message = " Somthing occured";
                    }
                    return View(trainerModel);
                }
                else
                {
                    return View(trainerModel);

                }
            }


            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();


            }

        }

      

        [HttpGet]        

        // GET: Admin/Trainer/Delete/5
        public ActionResult Delete(int ID)
        {
            Trainer trainer = Service.GetTrainerByID(ID); 
            if (trainer!=null)
            {
                TrainerModel trainerModel =mapper.Map<TrainerModel>(trainer);

                return View(trainerModel);

            }
            else
            {
                return View();
            }

        }

        // POST: Admin/Trainer/Delete/5
        [HttpPost]
        public ActionResult DeletedConfirm(int ID)
        {
            try
            {

                Service.Delete(ID);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
