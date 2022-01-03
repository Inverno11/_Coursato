using CoursesPlatform.Database.CoursesPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Services
{

    public interface IAdminServices 
    {
         bool Login(String Email, string Password);
        bool ChangePasswrd(String Email, string Password);
        bool ForgetPassword(String Email, string Password);



    }
    public class AdminServices : IAdminServices


    {
        public CoursesPlatformEntities context { set; get; }
        public AdminServices() {
            context = new CoursesPlatformEntities();


            }
        public bool ChangePasswrd(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ForgetPassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public bool Login(string Email, string Password)
        {
            return context.Admins.Where(x => x.Email == Email && x.Password == Password).Any();

        }
    }
}