//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoursesPlatform.Database.CoursesPlatform
{
    using System;
    using System.Collections.Generic;
    
    public partial class EnrolledCourse
    {
        public int CoursesID { get; set; }
        public int TraineeID { get; set; }
        public Nullable<System.DateTime> RegisterdDate { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}
