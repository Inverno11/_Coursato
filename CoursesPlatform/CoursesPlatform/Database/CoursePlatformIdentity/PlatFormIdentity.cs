using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Database.CoursePlatformIdentity
{
    public class PlatFormIdentity : IdentityDbContext<MyIdentity>
    {
        public PlatFormIdentity():base("PlatfromIdentityEntites")
        {

        }
    }

    public class MyIdentity : IdentityUser
    {
    
    }
}