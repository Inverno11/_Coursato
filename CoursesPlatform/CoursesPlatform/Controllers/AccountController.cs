using CoursesPlatform.Database.CoursePlatformIdentity;
using CoursesPlatform.Database.CoursesPlatform;
using CoursesPlatform.Models;
using CoursesPlatform.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesPlatform.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<MyIdentity> userManager;
        private readonly PlatFormIdentity dbIdentity;
        private readonly TraineeService traineeService;
        public AccountController()
        {
            dbIdentity = new PlatFormIdentity();
            var userStore = new UserStore<MyIdentity>(dbIdentity);
            userManager = new UserManager<MyIdentity>(userStore);
            traineeService = new TraineeService();

        }
        // GET: Accout
        public ActionResult Login(string ReturnUrl)
        {


            return View(new LoginViewModel { ReturnUrl = ReturnUrl }); 
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var userExist = userManager.Find(loginViewModel.Email, loginViewModel.Password);
                if (userExist != null)
                {
                    SignIn(userExist);

                    if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return Redirect(loginViewModel.ReturnUrl);
                    }

                    var roles = userManager.GetRoles(userExist.Id);
                    var userRoles = roles.FirstOrDefault();

                    if (userRoles == "Admins")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });

                    }

                    return RedirectToAction("Index", "Home");
                }

                loginViewModel.Message = "Email or Password is incorrect!";
            }

                return View(loginViewModel);
            }
        

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                MyIdentity identity = new MyIdentity();
                identity.UserName = registerViewModel.Email;
                identity.Email = registerViewModel.Email;

                var isCreated = userManager.Create(identity, registerViewModel.Password);
                if (isCreated.Succeeded)
                {
                    var addToRoleResult = userManager.AddToRoles(identity.Id, "Trainee");
                    if (addToRoleResult.Succeeded)
                    {
                        Trainee trainee = new Trainee();
                        trainee.Name = registerViewModel.Name;
                        trainee.Email = registerViewModel.Email;
                        trainee.IsActive = true;
                        trainee.Creation = System.DateTime.Now;
                        var reultOfAddOperation = traineeService.Add(trainee);
                        if (reultOfAddOperation == null)
                        {
                            registerViewModel.Message = "An Error while creating your account!";
                            return View(registerViewModel);

                        }

                        return RedirectToAction("Index", "Home");
                    }
                }


                registerViewModel.Message = isCreated.Errors.FirstOrDefault();
                return View(registerViewModel);
            }

            return View(registerViewModel);

        }

        private void SignIn(MyIdentity myIdentity)
        {

            var identity = userManager.CreateIdentity(myIdentity, DefaultAuthenticationTypes.ApplicationCookie);
            var owinContetxt = Request.GetOwinContext();
            var owinAuth = owinContetxt.Authentication;
            owinAuth.SignIn(identity);



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;
            authManager.SignOut("ApplicationCookie");
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }


    }





}
