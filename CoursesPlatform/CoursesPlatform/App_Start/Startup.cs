using CoursesPlatform.Database.CoursePlatformIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(CoursesPlatform.App_Start.Startup))]

namespace CoursesPlatform.App_Start
{
    public class Startup
    {
        private  UserManager<MyIdentity> userManager;
        private  PlatFormIdentity dbIdentity;

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            CookieAuthenticationOptions cookieAuthOptions = new CookieAuthenticationOptions();
            cookieAuthOptions.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            cookieAuthOptions.LoginPath = new PathString("/account/login");
            app.UseCookieAuthentication(cookieAuthOptions);





        }
        
    }
}
