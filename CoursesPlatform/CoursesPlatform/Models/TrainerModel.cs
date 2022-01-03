using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Models
{
    public class TrainerModel
    {

        public int ID { get; set; }
        [Required()]
        public string Name { get; set; }
        public System.DateTime Creation { get; set; }
        [StringLength(250,MinimumLength =10)]
        public string Description { get; set; }
        [Required()]
        [EmailAddress(ErrorMessage ="Enter your Email in correct format")]
        public string Email { get; set; }
        [Url(ErrorMessage = "Enter your Email in correct format")]
        public string Website { get; set; }


    }
}