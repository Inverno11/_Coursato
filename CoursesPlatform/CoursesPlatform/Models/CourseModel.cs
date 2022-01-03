using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Models
{
    public class CourseModel
    {

        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime Creation { get; set; }

        [Required]
        [Display(Name="Category")]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Trainer")]
        public Nullable<int> TrainerID { get; set; }
         public string TrainerName { get; set; }
        string _Image_ID; 

        public string Image_ID { set { if (string.IsNullOrWhiteSpace(value))
                {
                    _Image_ID= "empty.png";
                }    
            else
                {
                    _Image_ID = value;
                }
            
            
            } get { return _Image_ID; } }
        public HttpPostedFileBase ImageFile { get; set; }

        public string Description { get; set; }
        public SelectList Categories { set; get; }
        public SelectList Trainers { set; get; }


    }

    public class CourseModelList
    {

        public IEnumerable<CourseModel> CourseModels { set; get; }
        [Display(Name = "Course Filter")]

        public string Query { set; get; }
        [Display(Name = "Trainer")]
        public int TrainerID { set; get; }
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public SelectList Categories { get; set; }
        public SelectList Trainers { get; set; }


    }


        

}