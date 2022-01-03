using System.ComponentModel.DataAnnotations;

namespace CoursesPlatform.Areas.Admin.Models
{
    public class LoginModel
    {
        [EmailAddress (ErrorMessage = "Invalid Email Address")]
        [Required]
        [Display(Name ="Email address")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public string Message { get; set; }



    }
}