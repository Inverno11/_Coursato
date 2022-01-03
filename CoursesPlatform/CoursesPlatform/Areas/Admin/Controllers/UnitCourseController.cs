using AutoMapper;
using CoursesPlatform.App_Start;
using CoursesPlatform.Models;
using CoursesPlatform.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CoursesPlatform.Areas.Admin.Controllers
{
    public class UnitCourseController : Controller

    {

        UnitService unitService;
        IMapper mapper;

        public UnitCourseController()
        {
            unitService = new UnitService();
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Admin/UnitCourse
        public ActionResult Index(int? Id)
        {
            if (Id==null || Id==0)
            {
                return HttpNotFound();
            }

            var course_Units = unitService.ReadCourseUnits(Id.Value);
            var course_UnitsModels = mapper.Map<List<UnitModel>>(course_Units);


            return View(course_UnitsModels);
        }
    }
}