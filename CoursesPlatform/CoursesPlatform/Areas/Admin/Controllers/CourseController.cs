using AutoMapper;
using CoursesPlatform.App_Start;
using CoursesPlatform.Database.CoursesPlatform;
using CoursesPlatform.Models;
using CoursesPlatform.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class CourseController : Controller
    {

        private readonly IMapper mapper;
        private CourseService Service;
        private CategoryService categoryService;
        private TrainerService trainerService;




        public CourseController()
        {

            mapper = AutoMapperConfig.Mapper;
            Service = new CourseService();
            categoryService = new CategoryService();
            trainerService = new TrainerService(); 



        }
        // GET: Admin/Course
        public ActionResult Index(int? TrainerID , int? CategoryID , string Query)
        {

            CourseModelList courseModelData = new CourseModelList();
            InitaSelectedList(ref courseModelData);

          var AllCourse = Service.GetAllCourse(Query ,TrainerID ,CategoryID).ToList();
          var AllCourseModels=  mapper.Map<List<CourseModel>>(AllCourse);
            courseModelData.CourseModels = AllCourseModels;


            return View(courseModelData);
        }

        // GET: Admin/Course/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Course/Create
        public ActionResult Create()
        {
            var course = new CourseModel();
            InitaSelectedList( ref course);
            return View(course);
        }

        // POST: Admin/Course/Create
        [HttpPost]
        public ActionResult Create(CourseModel CourseModelData)
        {
            try
            {
                InitaSelectedList( ref CourseModelData);

                
                if (ModelState.IsValid)
                {

                    CourseModelData.Image_ID = SaveImageFile(CourseModelData.ImageFile,null);

                    var CourseDTO = mapper.Map<Course>(CourseModelData);

                    CourseDTO.Category = null;
                    CourseDTO.Trainer = null;

                  var added=  Service.Add(CourseDTO);
                    if (added>=1)
                    {
                        return RedirectToAction("Index");
                    }

                }
                return View(CourseModelData);

            }
            catch (Exception ex)
            {
                ViewBag.Message = " error occurred";
                return View(CourseModelData);
            }
        }

        // GET: Admin/Course/Edit/5
        public ActionResult Edit(int id)
        {
            var model = Service.GetCourseByID(id);
            var modelDTo = mapper.Map<CourseModel>(model);

            InitaSelectedList(ref modelDTo);

            return View(modelDTo) ;
        }

        // POST: Admin/Course/Edit/5
        [HttpPost]
        public ActionResult Edit(CourseModel courseModelData)
        {
            InitaSelectedList(ref courseModelData);

            try
            {
                if (ModelState.IsValid)
                {
                    courseModelData.Image_ID = SaveImageFile(courseModelData.ImageFile, courseModelData.Image_ID);

                    var CourseDTO = mapper.Map<Course>(courseModelData);
                    CourseDTO.Category = null;
                    CourseDTO.Trainer = null;

                    int added = Service.Edit(CourseDTO);

                    if (added >= 1)
                    {
                        return RedirectToAction("Index");

                    }
                }
                return View();

            }
            catch (Exception ex )
            {
                InitaSelectedList(ref courseModelData);
                ViewBag.Message = " error occurred";

                return View(courseModelData);
            }
        }

        // GET: Admin/Course/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Course/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public void InitaSelectedList(ref CourseModel courseModelData)
        {

            /* Get All category from database and assign 
             * them to CourseModel after convert them from Catogry Object to CategoryModel Object */


            var CatogoryListTDO = GetCategories();
            var SelectCatogoryListTDO = new SelectList(CatogoryListTDO, "ID", "Name");
            courseModelData.Categories = SelectCatogoryListTDO;

            /* Get All trainers from database and assign 
            * them to TrainerModel after convert them from Trainer Object to TrainerModel Object */
            var trainerListTDO = GetTrainers();
            var trainerListTDOList = new SelectList(trainerListTDO, "ID", "Name");
            courseModelData.Trainers = trainerListTDOList;

        }

        public void InitaSelectedList(ref CourseModelList courseModelListData)
        {

            /* Get All category from database and assign 
             * them to CourseModel after convert them from Catogry Object to CategoryModel Object */

            
            var CatogoryListTDO = GetCategories();
            var SelectCatogoryListTDO = new SelectList(CatogoryListTDO, "ID", "Name");
            courseModelListData.Categories = SelectCatogoryListTDO;

            /* Get All trainers from database and assign 
            * them to TrainerModel after convert them from Trainer Object to TrainerModel Object */
            var trainerListTDO = GetTrainers();
            var trainerListTDOList = new SelectList(trainerListTDO, "ID", "Name");
            courseModelListData.Trainers = trainerListTDOList;

        }

        private IEnumerable<CategoryModel> GetCategories()
        {
            var categories = categoryService.ReadAllCategory();
            return mapper.Map<IEnumerable<CategoryModel>>(categories);
        }

        private IEnumerable<TrainerModel> GetTrainers()
        {
            var trainers = trainerService.ReadAllTrainer();
            return mapper.Map<IEnumerable<TrainerModel>>(trainers);
        }


        public string SaveImageFile(HttpPostedFileBase httpPostedFileBase , string CurrentImageID ="") 
        {

            if (httpPostedFileBase != null)
            {
                string fileExtenction = Path.GetExtension(httpPostedFileBase.FileName);
                string imageGuid = Guid.NewGuid().ToString();
                string GuidFullPath = imageGuid + fileExtenction;
                string imagePath = Server.MapPath($"~/Uploads/Course/Images/{GuidFullPath}");
                httpPostedFileBase.SaveAs(imagePath);

                if (!string.IsNullOrEmpty(CurrentImageID))
                {

                    string oldPath = Server.MapPath($"~/Uploads/Course/Images/{CurrentImageID}");
                    System.IO.File.Delete(oldPath);
                }

                return GuidFullPath;

            }
            return CurrentImageID;

        }

    }
}
