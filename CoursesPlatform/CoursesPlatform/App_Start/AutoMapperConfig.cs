using AutoMapper;
using CoursesPlatform.Database.CoursesPlatform;
using CoursesPlatform.Models;

namespace CoursesPlatform.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        public static void Init()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryModel>()
                 .ForMember(dest => dest.ID, src => src.MapFrom(emp => emp.ID))
                 .ForMember(dest => dest.ParentID, src => src.MapFrom(emp => emp.Category2.ParentID))
                 .ForMember(dest => dest.ParentName, src => src.MapFrom(emp => emp.Category2.Name))
                 .ReverseMap();

                cfg.CreateMap<Trainer, TrainerModel>().ReverseMap();


                cfg.CreateMap<Course, CourseModel>().ForMember(dest => dest.CategoryName, src => src.MapFrom(co => co.Category.Name))
                .ForMember(des => des.TrainerName, src => src.MapFrom(co => co.Trainer.Name)).ReverseMap();

                cfg.CreateMap<Trainee, TraineeModel>().ReverseMap();
                cfg.CreateMap<EnrolledCourse, EnrolledCourseModel>().ForMember(dest => dest.CourseID, src => src.MapFrom(mo => mo.CoursesID))
                .ForMember(dest => dest.Registration, src => src.MapFrom(mo => mo.RegisterdDate))
                .ForMember(dest => dest.Trainee, src => src.MapFrom(mo => mo.Trainee)).ReverseMap();

                cfg.CreateMap<Course_Units, UnitModel>().ForMember(dest => dest.CourseName, src => src.MapFrom(e => e.Course.Name)).ReverseMap();


            });

            Mapper = mapperConfiguration.CreateMapper();






        }


    }
}