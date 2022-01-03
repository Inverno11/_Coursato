using AutoMapper;
using CoursesPlatform.App_Start;
using CoursesPlatform.Database.CoursesPlatform;
using CoursesPlatform.Models;
using CoursesPlatform.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CoursesPlatform.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        CoursesPlatformEntities entities;
        CourseService courseService;
        IMapper mapper;

        public HomeController()
        {
            entities = new CoursesPlatformEntities();
            courseService = new CourseService();
            mapper =  AutoMapperConfig.Mapper;


        }
        public ActionResult Index()
        {
            List<Course> AllCourses = courseService.GetAllCourse().ToList();
            List<CourseModel> AllModelsCourses = mapper.Map<List<CourseModel>>(AllCourses);
            return View(AllModelsCourses);
        }

        [HttpGet]
        public ActionResult IndexOfCourses()
        {

            List<Course> AllCourses = courseService.GetAllCourse().ToList();
            List<CourseModel> AllModelsCourses = mapper.Map<List<CourseModel>>(AllCourses);
            return View(AllModelsCourses);
        }
        public ActionResult CourseInfo(int? Id )
        {
            if(Id==null)
            {
                return HttpNotFound();
            }

            var course = courseService.GetCourseByID(Id.Value);
                if (course==null)
            {
                return HttpNotFound();

            }


            var courseModel = mapper.Map<CourseModel>(course);

            return View(courseModel);
        }

        public ActionResult EnrollCourse(int Id)
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            { 
                return RedirectToAction("Login", "Account", new { ReturnUrl = $"/Home/CourseInfo/{Id}" });
            }
            

        }

    }
}