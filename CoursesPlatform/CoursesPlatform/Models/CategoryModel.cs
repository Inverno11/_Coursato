using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Name Field is Required") ]
        [StringLength(200,MinimumLength = 1 ,ErrorMessage ="Wrong Field")]
        public string Name { get; set; }
        [DisplayName("Criteria")]
        public int? ParentID { get; set; }
        [DisplayName("Criteria")]
        public string ParentName { get; set; }

        public SelectList MainCategory { set; get; }


    }
}