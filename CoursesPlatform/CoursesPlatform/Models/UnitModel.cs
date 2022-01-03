using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class UnitModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CourseID { get; set; }
        public string CourseName { set; get; }
    }
}