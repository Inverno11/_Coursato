using CoursesPlatform.Areas.Admin.Models;
using CoursesPlatform.Services;
using System.Web.Mvc;
namespace CoursesPlatform.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            AdminServices AdminService = new AdminServices();
            var isLogged = AdminService.Login(loginModel.Email, loginModel.Password);


            if (isLogged)
            {
               return RedirectToAction("Index","Home");

            }
            else
            {
                loginModel.Message = "Email or password is not correct "+ AdminService.Login(loginModel.Email,
                    loginModel.Password) + loginModel.Email + " "+ loginModel.Password;
                return View(loginModel);
            }

        }
    }
}