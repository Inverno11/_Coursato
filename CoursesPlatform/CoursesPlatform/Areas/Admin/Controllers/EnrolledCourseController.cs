using AutoMapper;
using CoursesPlatform.App_Start;
using CoursesPlatform.Models;
using CoursesPlatform.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CoursesPlatform.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class EnrolledCourseController : Controller
    {
        private readonly IMapper mapper;
        private readonly EnrolledCourseService service;

        public EnrolledCourseController()
        {
            mapper = AutoMapperConfig.Mapper ;
            service = new EnrolledCourseService();
        }

        // GET: Admin/EnrolledCourse
        public ActionResult Index(int? ID)
        {
            if (ID==null)
            {

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            var EnrolledCoursesList = service.GetAllTrainees(ID.Value);
            var EnrolledCoursesModelList = mapper.Map<List<EnrolledCourseModel> >(EnrolledCoursesList);
            return View(EnrolledCoursesModelList);
        }
    }
}